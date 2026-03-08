using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum
{
    public static readonly StoryCheckEnum SongOfSwordAndWingsOfLostParadise =
        new(nameof(SongOfSwordAndWingsOfLostParadise), 30, StoryType.Mythos, "Song of Sword and Wings of Lost Paradise")
    {
    };

    public static readonly StoryCheckEnum ApexOfLogicAndCardinalScales =
        new(nameof(ApexOfLogicAndCardinalScales), 31, StoryType.Mythos, "The Apex of Logic and Cardinal Scales")
    {
    };

    public static readonly StoryCheckEnum ShadowOfSinAndSteel =
        new(nameof(ShadowOfSinAndSteel), 32, StoryType.Mythos, "Shadow of Sin and Steel")
    {
    };
}
