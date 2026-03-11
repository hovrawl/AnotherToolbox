using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Models.Team;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnotherToolBox.ViewModels.TeamBuilder;

public partial class CharacterStatusViewModel : ViewModelBase
{
    private readonly WikiService _wikiService;
    private readonly PlayerService _playerService;
    public ObservableCollection<CharacterStatDto> AtkStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> BaseStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> CritStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> TypeAtkStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> TypeResStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> StatusResStats { get; set; } = new();

    

    [ObservableProperty] private bool isLoading;
    [ObservableProperty] private bool equipmentLoading;
    
    [ObservableProperty] private EquipmentSlot? activeSlot;

    [ObservableProperty] private bool isEquipmentPickerOpen;

    public ObservableCollection<EquipmentItemViewModel> AvailableEquipment { get; } = new();
    
    public CharacterSlim Character { get; set; }
    
    public EquipmentSlot WeaponSlot { get; } = new(EquipmentType.Weapon, 0, "Weapon");
    public EquipmentSlot ArmorSlot { get; } = new(EquipmentType.Armor, 0, "Armor");

    public IReadOnlyList<EquipmentSlot> BadgeSlots { get; } =
    [
        new(EquipmentType.Badge, 0, "Badge 1"),
        new(EquipmentType.Badge, 1, "Badge 2")
    ];

    public IReadOnlyList<EquipmentSlot> GrastaSlots { get; } =
    [
        new(EquipmentType.Grasta, 0, "Grasta 1"),
        new(EquipmentType.Grasta, 1, "Grasta 2"),
        new(EquipmentType.Grasta, 2, "Grasta 3"),
        new(EquipmentType.Grasta, 3, "Grasta 4")
    ];

    public EquipmentSlot SpecialGrastaSlot { get; } = new(EquipmentType.Grasta, 0, "Special Grasta");

    public CharacterStatusViewModel(WikiService wikiService, PlayerService playerService)
    {
        _wikiService = wikiService;
        _playerService = playerService;
    }

    public async Task ConfigureCharacter(CharacterSlim character)
    {
        IsLoading = true;
        Character = character;
        
        // Get current stats 
        // Equipment effects
        var stats = await _wikiService.LoadCharacterStats(Character.PageId);

        // base stats
        BaseStats.Clear();
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Hp,
            Value = (double)stats.MaxHP
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Mp,
            Value = (double)stats.MaxMP
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Pwr,
            Value = (double)stats.MaxPwr
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Int,
            Value = (double)stats.MaxInt
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.End,
            Value = (double)stats.MaxEnd
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Spr,
            Value = (double)stats.MaxSpr
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Spd,
            Value = (double)stats.MaxSpd
        });
        BaseStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.Lck,
            Value = (double)stats.MaxLck
        });
        
        // Attack stats
        AtkStats.Clear();
        AtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PhysAtk,
            Value = 0
        });
        AtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.MagAtk,
            Value = 0
        });
        AtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PhysDef,
            Value = 0
        });
        AtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.MagDef,
            Value = 0
        });
        
        
        // Critical stats
        CritStats.Clear();
        CritStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PhyCrit,
            Value = 0
        });
        CritStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PhyCritStr,
            Value = 0
        });
        CritStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.MagCrit,
            Value = 0
        });
        CritStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.MagCritStr,
            Value = 0
        });
        
        
        // Type Atk stats
        TypeAtkStats.Clear();
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.FireAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.WaterAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.EarthAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.WindAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.ThunderAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.ShadeAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.CrystalAtk,
            Value = 0
        });
        TypeAtkStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.NullAtk,
            Value = 0
        });
        
        // Type Res stats
        TypeResStats.Clear();
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.FireRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.WaterRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.EarthRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.WindRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.ThunderRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.ShadeRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.CrystalRes,
            Value = 0
        });
        TypeResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.NullRes,
            Value = 0
        });
        
        
        // Status res stats
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.StunRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.SleepRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.StoneRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.FreezeRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PoisonRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.PainRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.DarknessRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.BindRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.ConfusionRes,
            Value = 10
        });
        StatusResStats.Add(new CharacterStatDto
        {
            Stat = UnitStat.BetrayalRes,
            Value = 10
        });

        
        IsLoading = false;
    }

    [RelayCommand]
    public async Task ChangeEquipment(EquipmentSlot slot)
    {
        if (slot is null)
            return;

        // slot.Type
        // slot.Slot
        ActiveSlot = slot;

        AvailableEquipment.Clear();

        var items = await LoadEquipmentForType(slot.Type);

        foreach (var item in items)
            AvailableEquipment.Add(item);

        IsEquipmentPickerOpen = true;
    }

    private async Task<IEnumerable<EquipmentItemViewModel>> LoadEquipmentForType(EquipmentType slotType)
    {
        // load equipment
        EquipmentLoading = true;
        var results = new List<EquipmentItemViewModel>();

        switch (slotType)
        {
            case EquipmentType.Weapon: 
            {
                if (_wikiService.CargoWeapons.Count == 0) await _wikiService.LoadWeapons();
                foreach (var wep in _wikiService.CargoWeapons)
                {
                    var wepModel = new EquipmentItemViewModel()
                    {
                        Type = slotType,
                        Name = wep.Name,
                        Id = wep.Id,
                    };
                    results.Add(wepModel);
                } 
                break;
            }
            case EquipmentType.Armor: 
            {
                if (_wikiService.CargoArmor.Count == 0) await _wikiService.LoadArmors();
                foreach (var wep in _wikiService.CargoArmor)
                {
                    var wepModel = new EquipmentItemViewModel()
                    {
                        Type = slotType,
                        Name = wep.Name,
                        Id = wep.Id,
                    };
                    results.Add(wepModel);
                }
                break;
            }
            case EquipmentType.Badge:
            {
                if (_playerService.Badges.Length == 0) _playerService.LoadBadgeData();
                foreach (var wep in _playerService.Badges)
                {
                    var wepModel = new EquipmentItemViewModel()
                    {
                        Type = slotType,
                        Name = wep.Name,
                        Id = wep.Name,
                    };
                    results.Add(wepModel);
                }
                break;
            }
            case EquipmentType.Grasta: 
            {
                if (_wikiService.CargoGrasta.Count == 0) await _wikiService.LoadGrasta();
                foreach (var wep in _wikiService.CargoGrasta)
                {
                    var wepModel = new EquipmentItemViewModel()
                    {
                        Type = slotType,
                        Name = wep.Name,
                        Id = wep.Id,
                    };
                    results.Add(wepModel);
                }
                break;
            }
        }

        EquipmentLoading = false;
        return results;
    }

    [RelayCommand]
    private void SelectEquipment(EquipmentItemViewModel item)
    {
        if (ActiveSlot is null || item is null)
            return;

        ActiveSlot.SelectedItem = item;
        IsEquipmentPickerOpen = false;

        ActiveSlot = null;
    }

    [RelayCommand]
    private void CloseEquipmentPicker()
    {
        IsEquipmentPickerOpen = false;
    }
}