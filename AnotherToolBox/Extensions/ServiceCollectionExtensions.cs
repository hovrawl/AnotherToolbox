using AnotherToolBox.Services;
using AnotherToolBox.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AnotherToolBox.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddCommonServices(this IServiceCollection collection)
    {
        // Required
        collection.AddLogging(logger => logger.AddConsole());
        collection.AddSingleton<WikiService>();
        
        // View Models
        collection.AddSingleton<MainWindowViewModel>();
        collection.AddSingleton<DashboardViewModel>();
        collection.AddSingleton<TeamBuilderViewModel>();
        return collection;
    }
}