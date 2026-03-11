using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AnotherToolBox.Models;

namespace AnotherToolBox.Data.Equipment;

public static class BadgeDataParser
{
    public static List<Badge> ParseBadgeFile(string filePath)
    {
        var badges = new List<Badge>();
        var lines = File.ReadAllLines(filePath);
        var currentBadgeText = string.Empty;

        foreach (var line in lines)
        {
            currentBadgeText += line.Trim();

            // Check if we have a complete badge entry (ends with }})
            if (currentBadgeText.Contains("}}"))
            {
                var badge = ParseBadgeEntry(currentBadgeText);
                if (badge != null)
                {
                    badges.Add(badge);
                }
                currentBadgeText = string.Empty;
            }
        }

        return badges;
    }

    private static Badge? ParseBadgeEntry(string badgeText)
    {
        // Extract name using regex: |name=<value>
        var nameMatch = Regex.Match(badgeText, @"\|name=([^|]+?)(?:\s*\||\s*}})");
        if (!nameMatch.Success)
            return null;

        // Extract effect using regex: |effect=<value>
        var effectMatch = Regex.Match(badgeText, @"\|effect=([^|]+?)(?:\s*\||\s*}})");
        if (!effectMatch.Success)
            return null;

        var effectString = effectMatch.Groups[1].Value.Trim();
        var effects = EffectParser.ParseEffect(effectString);
        
        return new Badge
        {
            Name = nameMatch.Groups[1].Value.Trim(),
            Effect = effects
        };
    }
}
