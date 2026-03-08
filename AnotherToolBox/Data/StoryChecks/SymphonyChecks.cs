using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum Persona5BoundWills =
        new(nameof(Persona5BoundWills), 38, StoryType.Symphony, "Persona 5")
    {
        SubTitle = "Bound Wills and the Hollow Puppeteer",
    };

    public static readonly StoryCheckEnum Persona5PromisesVowsAndRings =
        new(nameof(Persona5PromisesVowsAndRings), 39, StoryType.Symphony, "Persona 5")
    {
        SubTitle = "Bound Wills and the Hollow Puppeteer: Promises, Vows and Rings",
    };

    public static readonly StoryCheckEnum TalesOfChronographia =
        new(nameof(TalesOfChronographia), 40, StoryType.Symphony, "Tales Of...")
    {
        SubTitle = "Tails of Time and the Brave Four: Tales of Chronographia",
    };

    public static readonly StoryCheckEnum TalesOfCrownOfPaleDawn =
        new(nameof(TalesOfCrownOfPaleDawn), 41, StoryType.Symphony, "Tales Of...")
    {
        SubTitle = "Crown of the Pale Dawn",
    };

    public static readonly StoryCheckEnum ChronoCrossForgottenDream =
        new(nameof(ChronoCrossForgottenDream), 42, StoryType.Symphony, "Chrono Cross")
    {
        SubTitle = "The Forgotten Dream",
    };

    public static readonly StoryCheckEnum OctopathTravelerPrisonersOdyssey =
        new(nameof(OctopathTravelerPrisonersOdyssey), 43, StoryType.Symphony, "Octopath Traveler")
    {
        SubTitle = "Octolight Conductors: The Prisoner's Odyssey",
    };

    public static readonly StoryCheckEnum KingOfFightersAnotherBout =
        new(nameof(KingOfFightersAnotherBout), 44, StoryType.Symphony, "King of Fighters")
    {
        SubTitle = "The King of Fighters: Another Bout",
    };

    public static readonly StoryCheckEnum AtelierRyzaCrystalOfWisdom =
        new(nameof(AtelierRyzaCrystalOfWisdom), 45, StoryType.Symphony, "Atelier Ryza")
    {
        SubTitle = "Crystal of Wisdom and the Secret Castle",
    };

    public static readonly StoryCheckEnum FinalFantasyMemoriesOfAnotherSky =
        new(nameof(FinalFantasyMemoriesOfAnotherSky), 46, StoryType.Symphony, "Final Fantasy")
    {
        SubTitle = "Memories of Another Sky",
    };
}
