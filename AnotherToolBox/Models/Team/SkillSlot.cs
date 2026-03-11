using AnotherToolBox.ViewModels.TeamBuilder;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnotherToolBox.Models.Team;

public partial class SkillSlot : ObservableObject
{
    public int SlotIndex { get; }
    public string Label { get; }


    [ObservableProperty] private CharacterSkillViewModel selectedItem;
    
    public bool IsEmpty => SelectedItem is null;
    
    public bool IsSelected => !IsEmpty;
    public string? SelectedItemName => SelectedItem?.Name;


    public SkillSlot(int slotIndex, string label)
    {
        SlotIndex = slotIndex;
        Label = label;
    }

    partial void OnSelectedItemChanged(CharacterSkillViewModel? value)
    {
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(IsSelected));
        OnPropertyChanged(nameof(SelectedItemName));
    }
}

