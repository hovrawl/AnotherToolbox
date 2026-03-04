using System;
using System.Collections.Generic;
using System.Linq;
using AnotherToolBox.Services;
using AnotherToolBox.ViewModels;
using AnotherToolBox.ViewModels.Player;
using AnotherToolBox.Views.Controls;
using AnotherToolBox.Views.Controls.Player;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using Microsoft.Extensions.DependencyInjection;

namespace AnotherToolBox.Views;

public partial class MainWindow : AppWindow
{
    // Use WeakReference so cached views don't keep the objects alive forever
    // (they can be GC'd if no other strong references exist).
    private Dictionary<string, WeakReference<UserControl>> _viewCache = new();
    // When many distinct navigation keys are used, the dictionary can grow even
    // if many entries are dead (collected). Periodically prune to remove dead
    // references and keep memory usage bounded.
    private const int PruneThreshold = 50;

    public MainWindow()
    {
        InitializeComponent();
        Width = 660;
        Height = 400;
    }

    protected async override void OnOpened(EventArgs e)
    {
        try
        {
            if (NavigationView.MenuItems.FirstOrDefault(i => 
                    i is NavigationViewItem navItem 
                    && (navItem.Tag.ToString().ToLower() == "dashboard"
                        || navItem.Tag.ToString().ToLower() == "home")) is NavigationViewItem dashboardItem)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    NavigationView.SelectedItem = dashboardItem;

                    // Don't try to fake ItemInvoked / RaisePropertyChanged.
                    // Just run the same navigation logic you run on click.
                    if (dashboardItem.Tag is string tag)
                        NavigateTo(tag);
                }, DispatcherPriority.Loaded);
            }

            if (DataContext is not MainWindowViewModel vm) return;

            await vm.InitializeWiki();
        }
        catch (Exception ex)
        {
            throw; // TODO handle exception
        }
    }

    private void NavOnItemInvoked(object? sender, NavigationViewItemInvokedEventArgs e)
    {
        if (e.InvokedItemContainer is not NavigationViewItem item) return;
        if (item.Tag is not string tag) return;

        NavigateTo(tag);
    }

    private void NavigateTo(string tag)
    {
        if (DataContext is not MainWindowViewModel vm) return;

        // normalize the key so cache lookups are case-insensitive
        var key = tag.ToLowerInvariant();

        UserControl GetOrCreateView(string k, Func<UserControl> factory)
        {
            if (_viewCache.TryGetValue(k, out var wr) && wr.TryGetTarget(out var existing) && existing is not null)
                return existing;

            var created = factory();
            _viewCache[k] = new WeakReference<UserControl>(created);
            return created;
        }

        switch (key)
        {
            case "home":
            case "dashboard":
            {
                var dashboardView = GetOrCreateView(key, () =>
                {
                    // Create and wire up the VM when the view is first created.
                    var dashboardVm = vm.ServiceProvider.GetRequiredService<DashboardViewModel>();
                    return new DashboardView
                    {
                        DataContext = dashboardVm
                    };
                });

                // Set content to the (possibly cached) view instance
                NavigationView.Content = dashboardView;
                break;
            }
            case "team":
            {
                var teamView = GetOrCreateView(key, () =>
                {
                    var teamVm = vm.ServiceProvider.GetRequiredService<TeamBuilderViewModel>();
                    return new TeamBuilderView()
                    {
                        DataContext = teamVm
                    };
                });

                NavigationView.Content = teamView;
                break;
            }
            case "stories":
            {
                var teamView = GetOrCreateView(key, () =>
                {
                    var teamVm = vm.ServiceProvider.GetRequiredService<StoryChecksViewModel>();
                    return new StoryChecksView()
                    {
                        DataContext = teamVm
                    };
                });

                NavigationView.Content = teamView;
                break;
            }
        }

        // Keep the cache size in check by pruning dead entries occasionally.
        if (_viewCache.Count > PruneThreshold)
            PruneCache();
    }

    // Remove entries whose targets have been garbage collected.
    private void PruneCache()
    {
        var keysToRemove = new List<string>();
        foreach (var kvp in _viewCache)
        {
            if (!kvp.Value.TryGetTarget(out var _))
                keysToRemove.Add(kvp.Key);
        }

        foreach (var k in keysToRemove)
            _viewCache.Remove(k);
    }

    // Clear the cache and optionally dispose DataContexts of the alive views.
    public void ClearViewCache(bool disposeViewModels = false)
    {
        foreach (var kvp in _viewCache)
        {
            if (kvp.Value.TryGetTarget(out var view) && view is not null && disposeViewModels)
            {
                if (view.DataContext is IDisposable d)
                    d.Dispose();
            }
        }

        _viewCache.Clear();
    }

    // Remove a single cached view by key and optionally dispose its DataContext.
    public bool RemoveCachedView(string key, bool disposeViewModel = false)
    {
        if (string.IsNullOrEmpty(key)) return false;
        key = key.ToLowerInvariant();

        if (!_viewCache.TryGetValue(key, out var wr)) return false;

        if (wr.TryGetTarget(out var view) && view is not null && disposeViewModel)
        {
            if (view.DataContext is IDisposable d)
                d.Dispose();
        }

        return _viewCache.Remove(key);
    }
}