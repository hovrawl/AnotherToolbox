using AnotherToolBox.Services;
using AnotherToolBox.ViewModels;
using AnotherToolBox.ViewModels.Player;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnotherToolBox.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddCommonServices(this IServiceCollection collection)
    {
        // Required services
        collection.AddLogging(logger => logger.AddConsole());
        collection.AddSingleton<WikiService>();
        collection.AddSingleton<ImageService>();
        collection.AddSingleton<PlayerService>();
        
        // View Models (singletons so we dont duplicate)
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<DashboardViewModel>();
        collection.AddSingleton<TeamBuilderViewModel>();
        collection.AddSingleton<CharacterListViewModel>();
        collection.AddSingleton<StoryChecksViewModel>();
        
        // Scope the character frame as we have multiple
        collection.AddScoped<CharacterFrameViewModel>();
        
        return collection;
    }
}