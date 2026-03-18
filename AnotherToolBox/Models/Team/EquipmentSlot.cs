using AnotherToolBox.ViewModels.TeamBuilder;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnotherToolBox.Models.Team;

public partial class EquipmentSlot : ObservableObject
{
    public EquipmentType Type { get; }
    
    public string WeaponType { get; set; }
    
    public string ArmorType { get; set; }
    
    public int SlotIndex { get; }
    
    public string Label { get; }


    [ObservableProperty] private EquipmentItemViewModel selectedItem;
    
    public bool IsEmpty => SelectedItem is null;
    
    public bool IsSelected => !IsEmpty;
    public string? SelectedItemName => SelectedItem?.Name;


    public EquipmentSlot(EquipmentType type, int slotIndex, string label)
    {
        Type = type;
        SlotIndex = slotIndex;
        Label = label;
    }

    partial void OnSelectedItemChanged(EquipmentItemViewModel? value)
    {
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(IsSelected));
        OnPropertyChanged(nameof(SelectedItemName));
    }
}

