using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WikiClientLibrary.Cargo.Schema.DataAnnotations;

namespace AnotherToolBox.Models.Team;

public class Equipment
{
    public EquipmentType Type { get; set; }
    
    public string Name { get; set; }
    
    
}


public enum EquipmentType
{
    Weapon,
    Armor,
    Badge,
    Grasta
}

public enum WeaponType
{
    Ax,
    Bow,
    Fists,
    Hammer,
    Katana,
    Spear,
    Staff,
    Sword,
}

public enum ArmorType
{
    Bangle,
    Necklace,
    Ring
}

public struct StatEffect
{
    public UnitStat Stat;
    public double Value;
    public StatEffectType Effect;
    public bool Multiplicative;
}

public enum UnitStat
{
    Hp,
    Mp,
    Pwr, 
    Int, 
    End, 
    Spr,
    Spd,
    Lck,

    PhysAtk,
    MagAtk,
    PhysDef,
    MagDef,
    
    PhyCrit,
    PhyCritStr,
    MagCrit,
    MagCritStr,
    
    FireAtk,
    WaterAtk,
    EarthAtk,
    WindAtk,
    ThunderAtk,
    ShadeAtk,
    CrystalAtk,
    NullAtk,
    
    FireRes,
    WaterRes,
    EarthRes,
    WindRes,
    ThunderRes,
    ShadeRes,
    CrystalRes,
    NullRes,
    SlashRes,
    PiercingRes,
    BluntRes,
    MagicRes,
    
    StunRes,
    SleepRes,
    StoneRes,
    FreezeRes,
    PoisonRes,
    PainRes,
    DarknessRes,
    BindRes,
    ConfusionRes,
    BetrayalRes
}

public enum StatEffectType
{
    Increase,
    Decrease,
}

[Table("Weapons")]
public class WeaponCargo
{
    [Column("Id")]
    public string Id { get; set; }  
    [Column("Type")]
    public string Type { get; set; }  
    [Column("Name")]
    public string Name { get; set; }  

    public int? Level { get; set; }  
    [Column("Atk")]
    public int? Atk { get; set; }  
    [Column("MAtk")]
    public int? MAtk { get; set; }  
    [Column("Obtain")]
    public string Obtain { get; set; }  // - Wikitext
    
    [CargoList]
    [Column("SkillEnhance")]
    public ICollection<string> SkillEnhance { get; set; }
    
    public bool Unreleased { get; set; } 
}

[Table("Armor")]
public class ArmorCargo
{
    public string Type { get; set; }  
    public string Name { get; set; }  
    public int? Level { get; set; }  
    public int? Def { get; set; }  
    public int? MDef { get; set; }  

    public bool Unreleased { get; set; }
}

[Table("Grasta")]
public class GrastaCargo
{
    public string Image { get; set; }  
    public string Name { get; set; }  // - Wikitext - links to its own page I think
    public string Stat1 { get; set; }  
    public int? Value1 { get; set; }  
    public string Stat2 { get; set; }  
    public int? Value2 { get; set; }  
    public string Effect { get; set; }  // - Wikitext - 
    public string Upgrade { get; set; }  // - Wikitext - skills it upgrades
    public string Type { get; set; }  
    public string Obtain { get; set; }  // - Wikitext - obtain method
    public int? Tier { get; set; }  
    public string Connection { get; set; }  
    public bool Awakened { get; set; }  
    public string Personality { get; set; }  
    public bool Shareable { get; set; }
    public bool Unreleased { get; set; }
}

