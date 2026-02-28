using System;
using System.ComponentModel.DataAnnotations.Schema;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using WikiClientLibrary.Cargo.Schema;

namespace AnotherToolBox.Models.Characters;
[Table("Characters")]
public class CharacterSlim
{
    [Column(CargoSpecialColumnNames.PageName)]  
    public string Page { get; set; }  
    
    [Column("Id")]  
    public string Id { get; set; } = default!;  
  
    [Column("Name")]  
    public string Name { get; set; } = default!;

    [Column("ReleaseDate")]
    public DateTime? ReleaseDate { get; set; }
}