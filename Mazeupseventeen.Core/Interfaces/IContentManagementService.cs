using Mazeupseventeen.Core.Models.ApiModels;
using Umbraco.Cms.Core.Models;

namespace Mazeupseventeen.Core.Interfaces;

public interface IContentManagementService
{
    void CreateOrUpdateTvShow(TvMazeShow show);
    void Publish(IContent content);
}