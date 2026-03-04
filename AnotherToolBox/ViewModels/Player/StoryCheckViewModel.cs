using AnotherToolBox.Models.StoryChecks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnotherToolBox.ViewModels.Player;

public partial class StoryCheckViewModel: ViewModelBase
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    public StoryType StoryType { get; set; }
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    [ObservableProperty] private bool cleared;

    [RelayCommand]
    public void CheckClick()
    {
        Cleared = !Cleared;
    }
}