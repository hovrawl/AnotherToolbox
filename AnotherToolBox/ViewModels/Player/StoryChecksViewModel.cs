using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        var id = 1;
        foreach (var story in _playerService.StoryChecks)
        {
            var checkLists = new List<StoryCheckViewModel>();
            foreach (var check in story.Checks)
            {
                var newVm = new StoryCheckViewModel()
                {
                    Id = $"{story.Type}-{check.Id}",
                    Name = story.Name,
                    StoryType = story.Type,
                    Title = check.Title,
                    SubTitle = check.SubTitle,
                    Description = check.Description,
                    Image = check.Image,
                };
                checkLists.Add(newVm);

                AllCheckModels.Add(newVm);
            }
        }
        
        // groupedView.AddFilterProperty("Type");
        GroupedChecks = AllCheckModels.GroupBy(x => x.StoryType)
            .Select(g => new StoryCheckGroup(g.Key, g));
    }

}