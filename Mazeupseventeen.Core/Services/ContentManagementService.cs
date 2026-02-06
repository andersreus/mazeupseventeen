using Mazeupseventeen.Core.Interfaces;
using Mazeupseventeen.Core.Models.ApiModels;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Mazeupseventeen.Core.Services;

public class ContentManagementService : IContentManagementService
{
    private readonly IContentService _contentService;
    private readonly ILogger<ContentManagementService> _logger;

    public ContentManagementService(IContentService contentService, ILogger<ContentManagementService> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }
    
    public void CreateOrUpdateTvShow(TvMazeShow tvShow)                                                                                                         
    {                                                                                                                                                                      
        var root = GetTvShowsRoot();                                                                                                                                       
        if (root is null)                                                                                                                                                  
        {                                                                                                                                                                  
            _logger.LogWarning("TV Shows root node not found. Cannot create content.");
            return;
        }
        var existingContent = FindExistingTvShow(root.Id, tvShow.Id);

        var contentName = string.IsNullOrWhiteSpace(tvShow.Name) ? $"Show {tvShow.Id}" : tvShow.Name;

        if (existingContent is null)
        {
            var content = _contentService.Create(contentName, root.Key, "tvShow");
            SetTvShowProperties(content, tvShow);
            _contentService.Save(content);
            _logger.LogInformation("Created content with id: {Id} & name: {Name}", tvShow.Id, content.Name);
        }
        else if (HasChanges(existingContent, tvShow, contentName))
        {
            existingContent.Name = contentName;
            SetTvShowProperties(existingContent, tvShow);
            _contentService.Save(existingContent);
            _logger.LogInformation("Updated content with id: {Id} & name: {Name}", tvShow.Id, existingContent.Name);
        }
    }

    public void Publish(IContent content)
    {
        _contentService.Publish(content, null, -1);
    }
    
    // Helper methods, not part of the interface implementation.

    private IContent? GetTvShowsRoot()
    {
        var rootContent = _contentService.GetRootContent();
        return rootContent.FirstOrDefault(c => c.ContentType.Alias == "tvShows");
        // Remember to use the correct alias then
    }
    
    private IContent? FindExistingTvShow(int parentId, int tvShowId)
    { 
        var children = _contentService.GetPagedChildren(parentId, 0, int.MaxValue, out _);
        return children.FirstOrDefault(c => c.GetValue<int>("tvMazeId") == tvShowId);
    }
    
    private void SetTvShowProperties(IContent content, TvMazeShow tvShow)
    {
        content.SetValue("tvMazeId", tvShow.Id);
        content.SetValue("showName", tvShow.Name);
        content.SetValue("summary", tvShow.Summary);
    }
    // Remember to update HasChanges if more properties are added
    private bool HasChanges(IContent content, TvMazeShow tvShow, string contentName)
    {
        return content.Name != contentName ||
               content.GetValue<string>("showName") != tvShow.Name ||
               content.GetValue<string>("summary") != tvShow.Summary;
    }
}