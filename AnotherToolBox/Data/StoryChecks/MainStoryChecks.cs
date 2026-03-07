using AnotherToolBox.Models.StoryChecks;
using Ardalis.SmartEnum;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{

    public static readonly StoryCheckEnum MainStoryPartOne = 
        new(nameof(MainStoryPartOne), 1, StoryType.MainStory, "Main Story 1")
    {
        SubTitle = "Another Eden",
    };
}