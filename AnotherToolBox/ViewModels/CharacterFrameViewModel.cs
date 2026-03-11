using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Services;
using AnotherToolBox.ViewModels.TeamBuilder;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AnotherToolBox.ViewModels;

public partial class CharacterFrameViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;
    public string Id { get; } = Guid.NewGuid().ToString();
    [ObservableProperty]
    private CharacterChoiceDto? selectedCharacter;
    [ObservableProperty]
    private CharacterSlim? selectedCharacterSlim;
    
    [ObservableProperty] private bool selectorOpen;
    [ObservableProperty] private bool statsOpen;
    
    [ObservableProperty] private CharacterStatusViewModel characterStatusVm;

    public CharacterListViewModel CharacterListVm { get; set; }

    [RelayCommand]
    public void RemoveCharacter()
    {
        SelectedCharacter = null;
    }

    [RelayCommand]
    public async Task SelectCharacter()
    {
        SelectorOpen = !SelectorOpen;
        
       
        // When opening the selector, ensure characters are loaded (LoadCharacters is idempotent)
        if (SelectorOpen)
        {
            try
            {
                await CharacterListVm.LoadCharacters();
            }
            catch
            {
                // ignore here (or add logging), don't want UI to crash on load failure
            }
        } 
    }

    [RelayCommand]
    public async Task ViewStats()
    {
        if (SelectedCharacter == null) return;

        if (CharacterStatusVm == null)
        {
            
            var scope = _serviceProvider.CreateScope();
            var charStatusVm = scope.ServiceProvider.GetRequiredService<CharacterStatusViewModel>();
            CharacterStatusVm = charStatusVm;
        }

        if (CharacterStatusVm == null) return;

        await CharacterStatusVm.ConfigureCharacter(SelectedCharacterSlim);
        StatsOpen = true;
    }


    public CharacterFrameViewModel(CharacterListViewModel characterListVm, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        CharacterListVm = characterListVm;
        
        CharacterListVm.PropertyChanged += CharacterListVmOnPropertyChanged;
    }

    private void CharacterListVmOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CharacterListVm.SelectedCharacter))
        {
            if (SelectorOpen)
            {
                SelectedCharacter =  CharacterListVm.SelectedCharacter;
                SelectedCharacterSlim = CharacterListVm.SelectedCharacterSlim;
            }
        }
    }
}