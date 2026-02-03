using Mazeupseventeen.Core.Models.ApiModels;

namespace Mazeupseventeen.Core.Interfaces;

public interface ITvMazeApiService
{
    IAsyncEnumerable<TvMazeShow> GetAllTvShowsAsync();
    Task<TvMazeShow?> GetTvShowByIdAsync(int id);
    Task<TvMazeShow[]?> GetShowsPageAsync(int page);
}