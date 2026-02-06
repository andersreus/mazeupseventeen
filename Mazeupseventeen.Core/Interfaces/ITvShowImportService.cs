using Mazeupseventeen.Core.Models.ApiModels;

namespace Mazeupseventeen.Core.Interfaces;

public interface ITvShowImportService
{
    Task ImportAllShowsAsync();
    void ImportShow(TvMazeShow show);
}