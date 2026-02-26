using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WikiClientLibrary.Cargo.Schema;
using WikiClientLibrary.Cargo.Schema.DataAnnotations;

namespace AnotherToolBox.Models.Characters;

[Table("Characters")]  
public class Character  
{  
    [Column(CargoSpecialColumnNames.PageName)]  
    public string Page { get; set; }  
    
    [Column("Id")]  
    public string Id { get; set; } = default!;  
  
    [Column("Name")]  
    public string Name { get; set; } = default!;  
  
    [Column("SpoilerName")]  
    public string? SpoilerName { get; set; }  
  
    [Column("ReleaseDate")]  
    public DateTime? ReleaseDate { get; set; }  
  
    [Column("Element")]  
    public string? Element { get; set; }  
  
    [Column("WeaponType")]  
    public string? WeaponType { get; set; }  
  
    [Column("AccessoryType")]  
    public string? AccessoryType { get; set; }  
  
    [Column("MinRarity")]  
    public int MinRarity { get; set; }  
  
    [Column("MaxRarity")]  
    public int MaxRarity { get; set; }  
  
    [Column("Skills")]  
    [CargoList]  
    public IList<string> Skills { get; set; } = new List<string>();  
  
    [Column("Classes")]  
    [CargoList]  
    public IList<string> Classes { get; set; } = new List<string>();  
  
    [Column("Obtain")]  
    public string? Obtain { get; set; } // Wikitext; expand later if needed  
  
    [Column("Style")]  
    public int Style { get; set; }  
  
    [Column("IsAlter")]  
    public bool IsAlter { get; set; }  
  
    [Column("IsAwaken")]  
    public bool IsAwaken { get; set; }  
  
    [Column("Unreleased")]  
    public bool Unreleased { get; set; }  
  
    [Column("UpgradeType")]  
    public int UpgradeType { get; set; }  
}