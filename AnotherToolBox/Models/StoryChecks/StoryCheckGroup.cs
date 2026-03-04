using System.Collections.ObjectModel;
using System.Linq;
using AnotherToolBox.ViewModels.Player;

namespace AnotherToolBox.Models.StoryChecks;

public sealed class StoryCheckGroup
{
    public StoryCheckGroup(StoryType storyType, IGrouping<StoryType, StoryCheckViewModel> storyCheckViewModels)
    {
        Group = storyType;
        Items = new ObservableCollection<StoryCheckViewModel>(storyCheckViewModels);
    }

    public StoryType Group { get; }
    public ObservableCollection<StoryCheckViewModel> Items { get; }
}