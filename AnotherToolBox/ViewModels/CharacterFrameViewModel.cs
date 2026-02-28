using System;
using System.ComponentModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnotherToolBox.ViewModels;

public partial class CharacterFrameViewModel : ViewModelBase
{
    public string Id { get; } = Guid.NewGuid().ToString();
    [ObservableProperty]
    private CharacterChoiceDto? selectedCharacter;

    [ObservableProperty] private bool selectorOpen;
    
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

    public CharacterFrameViewModel(CharacterListViewModel characterListVm)
    {
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
            }
        }
    }
}