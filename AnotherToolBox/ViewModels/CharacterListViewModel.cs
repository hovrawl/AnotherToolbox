using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Services;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnotherToolBox.ViewModels;

public partial class CharacterListViewModel : ViewModelBase
{
    private readonly WikiService _wikiService;
    private readonly ImageService _imageService;


    public ObservableCollection<CharacterChoiceDto> Characters { get; } = new();
    
    [ObservableProperty]
    private bool isLoading;
    
    [ObservableProperty]
    private CharacterChoiceDto selectedCharacter;

    public CharacterSlim? SelectedCharacterSlim
    {
        get
        {
            return _wikiService.SlimCharacters.FirstOrDefault(c => c.Id == selectedCharacter?.Id);
        }
    }

    public CharacterListViewModel(WikiService wikiService, ImageService imageService)
    {
        _wikiService = wikiService;
        _imageService = imageService;
    }
    
    [RelayCommand]
    public async Task LoadCharacters()
    {
        await InitializeCharacters();
    }
    
    

    public async Task InitializeCharacters()
    {
        if (!_wikiService.Initialized) return;
        if (Characters.Count > 0) return;
        if (IsLoading) return;
        
        IsLoading = true;
        try
        {
            Characters.Clear();

            await _wikiService.LoadCharactersSlim();

            foreach (var character in _wikiService.SlimCharacters)
            {
                Characters.Add(new CharacterChoiceDto()
                {
                    Id =  character.Id,
                    Name = character.Name,
                    Style = character.Style,
                });
            }
            
            // Init weapons etc

            // await _wikiService.LoadWeapons();
            // await _wikiService.LoadArmors();
            // await _wikiService.LoadGrasta();
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task LoadCharacterImage(CharacterChoiceDto character)
    {
        if (character == null) return;

        try
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            character.IsLoading = true;

            var urls = await _wikiService.FetchCharacterThumbnails([character]).ConfigureAwait(false);
            var url = urls?.FirstOrDefault();
            if (string.IsNullOrEmpty(url)) return;

            var bitmap = await _imageService.GetBitmapFromUrlAsync(url, cancellationToken).ConfigureAwait(false);
            if (bitmap == null) return;

            // Set on UI thread so bindings are safe
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                // Assume CharacterSlim has a Bitmap property called Thumbnail (add it if necessary)
                character.Thumbnail = bitmap;
            });
        }
        finally
        {
            character.IsLoading = false;
        }
    }
}