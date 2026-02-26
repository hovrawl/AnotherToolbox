using AnotherToolBox.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AnotherToolBox.ViewModels;

public partial class DashboardViewModel: ViewModelBase
{
    private readonly WikiService _wikiService;

    public DashboardViewModel()
    {
        _wikiService = new WikiService(NullLoggerFactory.Instance.CreateLogger<WikiService>());
    }
    
    public DashboardViewModel(WikiService wikiService) : this()
    {
        _wikiService = wikiService;
    }
}