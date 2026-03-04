using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using AnotherToolBox.Models;
using AnotherToolBox.Models.StoryChecks;

namespace AnotherToolBox.Services;

public class PlayerService
{
    public StoryChecks[]? StoryChecks { get; private set; } = [];

    public PlayerService()
    {
        LoadStoryChecksData();
    }

    public void LoadStoryChecksData()
    {
        string filePath = Path.Combine("Resources", "StoryChecks.json");

        if (File.Exists(filePath))
        {
            try
            {
                var serialiserOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var jsonContent = File.ReadAllText(filePath);
                var storyChecks = JsonSerializer.Deserialize<StoryChecks[]>(jsonContent, serialiserOptions);
                StoryChecks = storyChecks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}