using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum HolySwordTwoKnights =
        new(nameof(HolySwordTwoKnights), 11, StoryType.Episode, "Holy Sword")
    {
        SubTitle = "Two Knights and the Holy Sword",
    };

    public static readonly StoryCheckEnum HolySwordFirstKnight =
        new(nameof(HolySwordFirstKnight), 12, StoryType.Episode, "Holy Sword")
    {
        SubTitle = "Firs Knight and the Holy Sword",
    };

    public static readonly StoryCheckEnum IdaSchoolPart1 =
        new(nameof(IdaSchoolPart1), 13, StoryType.Episode, "IDA School")
    {
        SubTitle = "IDA School Part 1",
        Description = "Absolute Zero Chain",
    };

    public static readonly StoryCheckEnum IdaSchoolPart2 =
        new(nameof(IdaSchoolPart2), 14, StoryType.Episode, "IDA School")
    {
        SubTitle = "IDA School Part 2",
        Description = "Butterfly's City and Heaven's Cradle",
    };

    public static readonly StoryCheckEnum IdaSchoolPart3 =
        new(nameof(IdaSchoolPart3), 15, StoryType.Episode, "IDA School")
    {
        SubTitle = "IDA School Part 3",
        Description = "Fruit of Guidance and the Lost Digital Child",
    };

    public static readonly StoryCheckEnum OceanPalace1000Year =
        new(nameof(OceanPalace1000Year), 16, StoryType.Episode, "Ocean Palace")
    {
        SubTitle = "The 1000 Year Ark of the Ocean Palace",
    };

    public static readonly StoryCheckEnum OceanPalace3000Realm =
        new(nameof(OceanPalace3000Realm), 17, StoryType.Episode, "Ocean Palace")
    {
        SubTitle = "The 3000 Realm Ark of the Sea Abyss",
    };

    public static readonly StoryCheckEnum TimeMineAndDreamers =
        new(nameof(TimeMineAndDreamers), 18, StoryType.Episode, "The Time Mine and the Dreamers")
    {
    };

    public static readonly StoryCheckEnum CelestialTowerAndShadowWitch =
        new(nameof(CelestialTowerAndShadowWitch), 19, StoryType.Episode, "The Celestial Tower and the Shadow Witch")
    {
    };

    public static readonly StoryCheckEnum AzureRebelClosedOffWorld =
        new(nameof(AzureRebelClosedOffWorld), 20, StoryType.Episode, "Azure Rebel")
    {
        SubTitle = "The Closed-off Open World and the Azure Rebel",
    };

    public static readonly StoryCheckEnum AzureRebelParadiseOfImperfections =
        new(nameof(AzureRebelParadiseOfImperfections), 21, StoryType.Episode, "Azure Rebel")
    {
        SubTitle = "A Paradise of Imperfections",
    };

    public static readonly StoryCheckEnum LostTomeAndSilverUnfadingFlower =
        new(nameof(LostTomeAndSilverUnfadingFlower), 22, StoryType.Episode, "The Lost Tome and the Silver Unfading Flower")
    {
    };

    public static readonly StoryCheckEnum WandererInTheBindingNight =
        new(nameof(WandererInTheBindingNight), 23, StoryType.Episode, "Wanderer in the Binding Night")
    {
    };

    public static readonly StoryCheckEnum WryzSagaPart1 =
        new(nameof(WryzSagaPart1), 24, StoryType.Episode, "Wryz Saga")
    {
        SubTitle = "Part 1",
        Description = "The Cliffs of Wyrmrest",
    };

    public static readonly StoryCheckEnum WryzSagaPart2 =
        new(nameof(WryzSagaPart2), 25, StoryType.Episode, "Wryz Saga")
    {
        SubTitle = "Part 2",
        Description = "The Wings of Destiny",
    };

    public static readonly StoryCheckEnum WryzSagaPart3 =
        new(nameof(WryzSagaPart3), 26, StoryType.Episode, "Wryz Saga")
    {
        SubTitle = "Part 3",
        Description = "The Mists of Myth",
    };

    public static readonly StoryCheckEnum OdeToOriginAct1 =
        new(nameof(OdeToOriginAct1), 27, StoryType.Episode, "Ode to Origin")
    {
        SubTitle = "Act 1",
        Description = "The Beastfolk \"Antimony\"",
    };

    public static readonly StoryCheckEnum OdeToOriginAct2 =
        new(nameof(OdeToOriginAct2), 28, StoryType.Episode, "Ode to Origin")
    {
        SubTitle = "Act 2",
        Description = "The Humans \"A Just Dream\"",
    };

    public static readonly StoryCheckEnum OdeToOriginAct3 =
        new(nameof(OdeToOriginAct3), 29, StoryType.Episode, "Ode to Origin")
    {
        SubTitle = "Act 3",
        Description = "The Demi-Humans \"Eudaemonism\"",
    };
}
