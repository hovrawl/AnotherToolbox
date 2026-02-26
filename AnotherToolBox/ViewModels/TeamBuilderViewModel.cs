using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AnotherToolBox.ViewModels;

public partial class TeamBuilderViewModel: ViewModelBase
{
    private readonly WikiService _wikiService;

    
    public ObservableCollection<CharacterSlim> Characters { get; } = new();
    
    [ObservableProperty]
    private bool isLoading;
    
    public TeamBuilderViewModel()
    {
        _wikiService = new WikiService(NullLoggerFactory.Instance.CreateLogger<WikiService>());
    }
    
    public TeamBuilderViewModel(WikiService wikiService) : this()
    {
        _wikiService = wikiService;
    }
    

    [RelayCommand]
    public async Task LoadCharacters()
    {
        await InitializeCharacters();
    }
    
    

    public async Task InitializeCharacters()
    {
        if (!_wikiService.Initialized) return;
        IsLoading = true;
        try
        {
            Characters.Clear();

            await _wikiService.LoadCharactersSlim();

            foreach (var character in _wikiService.SlimCharacters)
            {
                Characters.Add(character);
            }
        }
        finally
        {
            IsLoading = false;
        }
    }
}