using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum LittlePrincessBigAdventure =
        new(nameof(LittlePrincessBigAdventure), 47, StoryType.Other, "Little Princess' Big Adventure")
    {
    };

    public static readonly StoryCheckEnum ForeignSkiesAndShipToFreedom =
        new(nameof(ForeignSkiesAndShipToFreedom), 48, StoryType.Other, "Foreign Skies and the Ship to Freedom")
    {
    };
}
