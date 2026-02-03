using System.Net.Http.Json;
using Mazeupseventeen.Core.Interfaces;
using Mazeupseventeen.Core.Models.ApiModels;
using Microsoft.Extensions.Logging;

namespace Mazeupseventeen.Core.Services;

public class TvMazeApiApiService : ITvMazeApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    
    public TvMazeApiApiService(
        HttpClient httpClient,
        ILogger<TvMazeApiApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async IAsyncEnumerable<TvMazeShow> GetAllTvShowsAsync()
    {
        var page = 0;
        while (true)
        {
            _logger.LogInformation("Fetching shows page {Page}", page);

            var shows = await GetShowsPageAsync(page);
            if (shows is null || shows.Length == 0)
            {
                _logger.LogInformation("No more shows found at page {Page}. Import complete.", page);
                yield break;
            }
            
            foreach (var show in shows)
            {
                yield return show;
            }
            
            page++;
            // Add delay?
        }
    }

    public Task<TvMazeShow?> GetTvShowByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<TvMazeShow[]?> GetShowsPageAsync(int page)
    {
        try
        {
            var response = await _httpClient.GetAsync($"shows?page={page}");
            
            if (response.StatusCode is System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TvMazeShow[]>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get shows page {Page}", page);
            return null;
        }
    }
}