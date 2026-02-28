using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnotherToolBox.Models.Characters;

public partial class CharacterChoiceDto: ObservableObject
{

    public string Id { get; set; } = default!;  
  

    public string Name { get; set; } = default!;
    
    public int Style { get; set; }

    [ObservableProperty] private Bitmap thumbnail;

    [ObservableProperty] private bool isLoading;
}