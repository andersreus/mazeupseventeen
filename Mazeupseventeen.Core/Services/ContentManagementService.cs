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
    
    public async Task CreateOrUpdateTvShowAsync(TvMazeShow tvShow)
    {
        var root = GetTvShowsRoot();
        if (root is null)
        {
            _logger.LogWarning("TV Shows root node not found. Cannot create content.");
            return;
        }
        var exstingContent = FindExistingTvShow(root.Id, tvShow.Id);

        IContent content;
        var contentName = string.IsNullOrWhiteSpace(tvShow.Name) ? $"Show {tvShow.Id}" : tvShow.Name;
        
        if (exstingContent is null)
        {
            content = _contentService.Create(contentName, root.Key, "tvShow");
        }
        else
        {
            content = exstingContent;
            content.Name = contentName;
        }
        SetTvShowProperties(content, tvShow);
        _contentService.Save(content);
        
        _logger.LogInformation($"Created content with id: {content.Id} & name: {content.Name}");
    }

    public Task PublishAsync(int tvShowId)
    {
        throw new NotImplementedException();
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
        // Extend model with the rest of the properties if it's working
        content.SetValue("tvMazeId", tvShow.Id);
        content.SetValue("showName", tvShow.Name);
        content.SetValue("summary", tvShow.Summary);
    }
}