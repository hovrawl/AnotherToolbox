using AnotherToolBox.Data.StoryChecks;
using AnotherToolBox.Models.StoryChecks;
using AnotherToolBox.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnotherToolBox.ViewModels.Player;

public partial class StoryCheckViewModel: ViewModelBase
{
    private readonly PlayerService _playerService;
    public string Name { get; set; }
    
    public StoryType StoryType { get; set; }
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public StoryCheckEnum StoryCheck { get; set; }

    [ObservableProperty] private bool cleared;

    [RelayCommand]
    public void CheckClick()
    {
        Cleared = !Cleared;
        
        // persist
        _playerService.SaveStoryChecksData(StoryCheck.Value, Cleared);
    }

    public StoryCheckViewModel(PlayerService playerService)
    {
        _playerService = playerService;

    }
    
}