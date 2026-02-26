using System.ComponentModel.DataAnnotations.Schema;

namespace AnotherToolBox.Models.Characters;

[Table("CharacterPersonality")]  
public class CharacterPersonality  
{  
    [Column("Personality")]  
    public string Personality { get; set; } = default!;  

    [Column("UnlockCondition")]  
    public string UnlockCondition { get; set; } = default!; // Wikitext maps to string  

    [Column("PersonalityIndex")]  
    public int PersonalityIndex { get; set; }  
}
