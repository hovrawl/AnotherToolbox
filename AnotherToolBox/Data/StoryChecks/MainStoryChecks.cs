using AnotherToolBox.Models.StoryChecks;
using Ardalis.SmartEnum;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum MainStory1 =
        new(nameof(MainStory1), 1, StoryType.MainStory, "Main Story 1")
    {
        SubTitle = "Another Eden",
    };

    public static readonly StoryCheckEnum MainStory1_5 =
        new(nameof(MainStory1_5), 2, StoryType.MainStory, "Main Story 1.5")
    {
        SubTitle = "Ogre Wars",
    };

    public static readonly StoryCheckEnum MainStory2Part1 =
        new(nameof(MainStory2Part1), 3, StoryType.MainStory, "Main Story 2")
    {
        SubTitle = "Part 1",
        Description = "Return of the Goddess of Time: Tales from the East",
    };

    public static readonly StoryCheckEnum MainStory2Part2 =
        new(nameof(MainStory2Part2), 4, StoryType.MainStory, "Main Story 2")
    {
        SubTitle = "Part 2",
        Description = "Return of the Goddess of Time: Tales from the East",
    };

    public static readonly StoryCheckEnum MainStory2Part3_TheTwist =
        new(nameof(MainStory2Part3_TheTwist), 5, StoryType.MainStory, "Main Story 2")
    {
        SubTitle = "Part 3",
        Description = "Return of the Goddess of Time: The Twist",
    };

    public static readonly StoryCheckEnum MainStory2Part3_Conclusion =
        new(nameof(MainStory2Part3_Conclusion), 6, StoryType.MainStory, "Main Story 2")
    {
        SubTitle = "Part 3",
        Description = "Return of the Goddess of Time: Conclusion",
    };

    public static readonly StoryCheckEnum MainStory3Part1 =
        new(nameof(MainStory3Part1), 7, StoryType.MainStory, "Main Story 3")
    {
        SubTitle = "Part 1",
        Description = "The Chronos Empire Strikes Back",
    };

    public static readonly StoryCheckEnum MainStory3Part2 =
        new(nameof(MainStory3Part2), 8, StoryType.MainStory, "Main Story 3")
    {
        SubTitle = "Part 2",
        Description = "The Chronos Empire Strikes Back",
    };

    public static readonly StoryCheckEnum MainStory3Part3 =
        new(nameof(MainStory3Part3), 9, StoryType.MainStory, "Main Story 3")
    {
        SubTitle = "Part 3",
        Description = "The Chronos Empire Strikes Back",
    };

    public static readonly StoryCheckEnum MainStory3Part4 =
        new(nameof(MainStory3Part4), 10, StoryType.MainStory, "Main Story 3")
    {
        SubTitle = "Part 4",
        Description = "The Chronos Empire Strikes Back",
    };
}