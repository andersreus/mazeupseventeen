using Mazeupseventeen.Core.Models.ApiModels;

namespace Mazeupseventeen.Core.Interfaces;

public interface IContentManagementService
{
    Task CreateOrUpdateTvShowAsync(TvMazeShow show);
    Task PublishAsync(int tvShowId);
}