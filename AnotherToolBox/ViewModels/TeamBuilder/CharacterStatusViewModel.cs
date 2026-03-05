using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Models.Team;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnotherToolBox.ViewModels.TeamBuilder;

public partial class CharacterStatusViewModel : ViewModelBase
{
    private readonly WikiService _wikiService;
    public ObservableCollection<CharacterStatDto> AtkStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> BaseStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> CritStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> TypeAtkStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> TypeResStats { get; set; } = new();
    public ObservableCollection<CharacterStatDto> StatusResStats { get; set; } = new();

    

    [ObservableProperty] private bool isLoading;
    
    public CharacterSlim Character { get; set; }
    
    public CharacterStatusViewModel(WikiService wikiService)
    {
        _wikiService = wikiService;
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
}