using System.Linq;
using AnotherToolBox.Models.Team;

namespace AnotherToolBox.Models;

public class Badge
{
    public string Name { get; set; } = string.Empty;
    public StatEffect[] Effect { get; set; } = [];

    public override string ToString()
    {
        return Name + " {" + Effect.Select(i => i.ToString()).Aggregate((a, b) => a + ", " + b) + "}";
    }
}
