namespace AnotherToolBox.Models.StoryChecks;

public class StoryChecks
{
    public StoryType Type { get; set; }
    public string? Name { get; set; }
    public StoryCheck[]? Checks { get; set; }
}