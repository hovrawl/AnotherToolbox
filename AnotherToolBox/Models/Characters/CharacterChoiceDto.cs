using Avalonia.Media.Imaging;

namespace AnotherToolBox.Models.Characters;

public class CharacterChoiceDto
{

    public string Id { get; set; } = default!;  
  

    public string Name { get; set; } = default!;
    
    public Bitmap Thumbnail { get; set; }
}