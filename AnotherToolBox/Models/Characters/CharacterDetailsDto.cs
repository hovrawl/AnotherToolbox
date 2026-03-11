using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WikiClientLibrary.Cargo.Schema;
using WikiClientLibrary.Cargo.Schema.DataAnnotations;

namespace AnotherToolBox.Models.Characters;

[Table("Characters")]  
public class CharacterDetailsDto
{
    [Column(CargoSpecialColumnNames.PageName)]  
    public string Page { get; set; }  
    
    [Column("Id")]  
    public string Id { get; set; } = default!;  
  
    [Column("Name")]  
    public string Name { get; set; } = default!;  
    
    [Column("Element")]  
    public string? Element { get; set; }  
  
    [Column("WeaponType")]  
    public string? WeaponType { get; set; }  
  
    [Column("AccessoryType")]  
    public string? AccessoryType { get; set; }  
    
    [Column("Skills")]  
    [CargoList]  
    public IList<string> Skills { get; set; } = new List<string>();  

}