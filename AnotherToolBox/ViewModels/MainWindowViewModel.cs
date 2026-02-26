using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AnotherToolBox.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly WikiService _wikiService;
    public readonly IServiceProvider ServiceProvider;

    public ObservableCollection<CharacterSlim> Characters { get; } = new();

    public MainWindowViewModel()
    {
        _wikiService = new WikiService(NullLoggerFactory.Instance.CreateLogger<WikiService>());
    }
    
    public MainWindowViewModel(WikiService wikiService, IServiceProvider serviceProvider) : this()
    {
        _wikiService = wikiService;
        ServiceProvider = serviceProvider;
    }

    [RelayCommand]
    public async Task LoadCharacters()
    {
        await InitializeCharacters();
    }
    
    public async Task InitializeWiki()
    {
        if (_wikiService.Initialized) return;
        await _wikiService.Setup();
    }

    public async Task InitializeCharacters()
    {
        if (!_wikiService.Initialized) return;
        Characters.Clear();

        await _wikiService.LoadCharactersSlim();

        foreach (var character in _wikiService.SlimCharacters)
        {
            Characters.Add(character);
        }
    }
}