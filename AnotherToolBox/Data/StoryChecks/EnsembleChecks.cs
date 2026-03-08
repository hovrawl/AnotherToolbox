using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum HeroesOfBygoneDaysAndEngravedOath =
        new(nameof(HeroesOfBygoneDaysAndEngravedOath), 34, StoryType.Ensemble, "Heroes of Bygone Days and the Engraved Oath")
    {
    };

    public static readonly StoryCheckEnum ScytheOfRebirthAndCrimsonFlower =
        new(nameof(ScytheOfRebirthAndCrimsonFlower), 35, StoryType.Ensemble, "Scythe of Rebirth and the Crimson Flower")
    {
    };

    public static readonly StoryCheckEnum CommanderAndSheep =
        new(nameof(CommanderAndSheep), 36, StoryType.Ensemble, "Commander and Sheep")
    {
    };

    public static readonly StoryCheckEnum WhereLightDoesNotFall =
        new(nameof(WhereLightDoesNotFall), 37, StoryType.Ensemble, "Where Light Does Not Fall")
    {
    };
}
