using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum WandererInTheVortex =
        new(nameof(WandererInTheVortex), 33, StoryType.Apocrypha, "Wanderer in the Vortex")
    {
    };
}
