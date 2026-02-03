using Mazeupseventeen.Core.Models.ApiModels;

namespace Mazeupseventeen.Core.Interfaces;

public interface ITvShowImportService
{
    Task ImportAllShowsAsync();
    Task ImportShowAsync(TvMazeShow show);
}