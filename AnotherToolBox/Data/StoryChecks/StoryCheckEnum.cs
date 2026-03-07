using AnotherToolBox.Models.StoryChecks;
using Ardalis.SmartEnum;

namespace AnotherToolBox.Data.StoryChecks;

public partial class StoryCheckEnum : SmartEnum<StoryCheckEnum>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? SubTitle { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public StoryType Type { get; set; }
    
    public StoryCheckEnum(string name,
        int value,
        StoryType storyType,
        string title) : base(name, value)
    {
        Type = storyType;
        Title = title;
    }
}