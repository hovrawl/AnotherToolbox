using System.ComponentModel.DataAnnotations.Schema;
using WikiClientLibrary.Cargo.Schema;

namespace AnotherToolBox.Models.Characters;

[Table("CharacterStats")]
public class CharacterStatsCargo
{
    [Column(CargoSpecialColumnNames.PageName)]  
    public string Page { get; set; }  
    
    [Column(CargoSpecialColumnNames.PageId)]  
    public string PageId { get; set; } 
    
    [Column(CargoSpecialColumnNames.Id)]  
    public string CharId { get; set; } 
    
    public int? MinHP { get; set; }
    public int? MaxHP { get; set; }
    public int? MinMP { get; set; }
    public int? MaxMP { get; set; }
    public int? MinPwr { get; set; }
    public int? MaxPwr { get; set; }
    public int? MinInt { get; set; }
    public int? MaxInt { get; set; }
    public int? MinEnd { get; set; }
    public int? MaxEnd { get; set; }
    public int? MinSpd { get; set; }
    public int? MaxSpd { get; set; }
    public int? MinLck { get; set; }
    public int? MaxLck { get; set; }
    public int? MinSpr { get; set; }
    public int? MaxSpr { get; set; }
    public int? SaHP { get; set; }
    public int? SaMP { get; set; }
    public int? SaPwr { get; set; }
    public int? SaInt { get; set; }
    public int? SaEnd { get; set; }
    public int? SaSpd { get; set; }
    public int? SaLck { get; set; }
    public int? SaSpr { get; set; }
}