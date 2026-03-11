using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using AnotherToolBox.Data.Equipment;
using AnotherToolBox.Models;
using AnotherToolBox.Models.StoryChecks;
using AnotherToolBox.ViewModels.Player;

namespace AnotherToolBox.Services;

public class PlayerService
{
    private readonly ConcurrentDictionary<int, bool> _pendingStoryChecks = new();
    private Timer? _debounceTimer;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private const int DebounceDelayMs = 1000;
    private string StoryChecksDataPath = Path.Combine("Resources", "StoryChecks.json");
    private string StoryChecksStatusPath = Path.Combine("Resources", "StoryChecksStatus.json");
    private string BadgesDbPath = Path.Combine("Resources", "Badges.json");
    private string BadgeSourcePath = Path.Combine("Resources", "badges.txt");

    public StoryChecks[]? StoryChecks { get; private set; } = [];

    public Badge[]? Badges { get; private set; } = [];
    
    public PlayerService()
    {
        LoadStoryChecksData();
        LoadBadgeData();
    }

    public void LoadStoryChecksData()
    {
        var serialiserOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        if (File.Exists(StoryChecksDataPath))
        {
            try
            {
               
                var jsonContent = File.ReadAllText(StoryChecksDataPath);
                var storyChecks = JsonSerializer.Deserialize<StoryChecks[]>(jsonContent, serialiserOptions);
                StoryChecks = storyChecks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        if (File.Exists(StoryChecksStatusPath))
        {
            try
            {
                var jsonContent = File.ReadAllText(StoryChecksStatusPath);
                var storyChecks = JsonSerializer.Deserialize<StoryCheckStatus[]>(jsonContent, serialiserOptions);

                foreach (var status in storyChecks)
                {
                    _pendingStoryChecks[status.Id] = status.Cleared;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }


    public void SaveStoryChecksData(int id, bool cleared)
    {
        _pendingStoryChecks[id] = cleared;

        _debounceTimer?.Dispose();
        _debounceTimer = new Timer(_ =>
        {
            _ = WriteStoryChecksDataAsync();
        }, null, DebounceDelayMs, Timeout.Infinite);
    }

    private async Task WriteStoryChecksDataAsync()
    {
        if (_cancellationTokenSource.Token.IsCancellationRequested)
            return;

        var checkStatus = new List<StoryCheckStatus>();

        foreach (var kvp in _pendingStoryChecks)
        {
            var status = new StoryCheckStatus()
            {
                Id = kvp.Key,
                Cleared = kvp.Value
            };
            checkStatus.Add(status);
        }

        var jsonContent = JsonSerializer.Serialize(checkStatus);
        await File.WriteAllTextAsync(StoryChecksStatusPath, jsonContent, _cancellationTokenSource.Token);
    }

    public void Dispose()
    {
        _debounceTimer?.Dispose();
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }


    public bool IsStoryCleared(int id)
    {
        _pendingStoryChecks.TryGetValue(id, out var cleared);
        return cleared;
    }
    
    public void LoadBadgeData()
    {
        var serialiserOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        if (File.Exists(BadgesDbPath))
        {
            try
            {
               
                var jsonContent = File.ReadAllText(BadgesDbPath);
                var result = JsonSerializer.Deserialize<Badge[]>(jsonContent, serialiserOptions);
                Badges = result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // If we have badges already we can load from source
        if (Badges?.Length > 0) return;
        
        if (File.Exists(BadgeSourcePath))
        {
            try
            {
                // Use parser
                var badges = BadgeDataParser.ParseBadgeFile(BadgeSourcePath);
                Badges = badges.ToArray();
                
                // Update badge db
                
                var jsonContent = JsonSerializer.Serialize(Badges);
                File.WriteAllText(BadgesDbPath, jsonContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}