using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AnotherToolBox.Data.StoryChecks;
using AnotherToolBox.Models.StoryChecks;
using AnotherToolBox.Services;
using Avalonia.Collections;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Data;

namespace AnotherToolBox.ViewModels.Player;

public partial class StoryChecksViewModel: ViewModelBase
{
    private readonly PlayerService _playerService;


    public ObservableCollection<StoryCheckViewModel> AllCheckModels { get; } = new();

    [ObservableProperty] private IEnumerable<StoryCheckGroup> groupedChecks;

    public StoryChecksViewModel(PlayerService playerService)
    {
        _playerService = playerService;

        var allChecks = StoryCheckEnum.List;
        foreach (var story in allChecks)
        {
            var checkLists = new List<StoryCheckViewModel>();
            var newVm = new StoryCheckViewModel(playerService)
            {
                StoryCheck = story,
                Name = story.Name,
                StoryType = story.Type,
                Title = story.Title,
                SubTitle = story.SubTitle,
                Description = story.Description,
                Image = story.Image,
                Cleared = playerService.IsStoryCleared(story.Value)
            };
            checkLists.Add(newVm);

            AllCheckModels.Add(newVm);
        }
        
        // groupedView.AddFilterProperty("Type");
        GroupedChecks = AllCheckModels.GroupBy(x => x.StoryType)
            .Select(g => new StoryCheckGroup(g.Key, g));
    }

}