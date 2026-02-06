using Mazeupseventeen.Core.Interfaces;
using Mazeupseventeen.Core.Models.ApiModels;
using Microsoft.Extensions.Logging;

namespace Mazeupseventeen.Core.Services;

public class TvShowImportService : ITvShowImportService
{
    private readonly ILogger<TvShowImportService> _logger;
    private readonly ITvMazeApiService _tvMazeApiService;
    private readonly IContentManagementService _contentManagementService;

    public TvShowImportService(
        ILogger<TvShowImportService> logger,
        ITvMazeApiService tvMazeApiService,
        IContentManagementService contentManagementService)
    {
        _logger = logger;
        _tvMazeApiService = tvMazeApiService;
        _contentManagementService = contentManagementService;
    }
    public async Task ImportAllShowsAsync()
    {
        _logger.LogInformation("Starting tv show import");

        try
        {
            await foreach (var show in _tvMazeApiService.GetAllTvShowsAsync())
            {
                ImportShow(show);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Failed to import show");
        }
        
        _logger.LogInformation("Completed tv show import");
    }

    public void ImportShow(TvMazeShow show)
    {
        _contentManagementService.CreateOrUpdateTvShow(show);
    }
}