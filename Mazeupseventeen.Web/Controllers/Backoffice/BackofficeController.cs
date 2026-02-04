using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Mazeupseventeen.Web.Controllers.Backoffice;

[ApiController]
[Route("api/[controller]")]
public class BackofficeController : Controller
{
    private readonly IContentService _contentService;
    private readonly ILogger<BackofficeController> _logger;
    
    public BackofficeController(IContentService contentService, ILogger<BackofficeController> logger)
    {
        _contentService = contentService;
        _logger = logger;
    }

    [HttpGet("getamountoftvshowsinbackoffice")]
    public int GetAmountOfTVShowsInBackOffice()
    {
        int count = _contentService.Count("tvShow");
        return count;
    }

    [HttpPost("deletealltvshowsinbackoffice")]
    public IActionResult DeleteAllTVShowsInBackOffice()
    {
        var tvShowsRoot = GetTvShowsRoot();
        if (tvShowsRoot == null)
        {
            return NotFound("TV Shows root content node not found.");
        }
        var tvShowContents = _contentService.GetPagedChildren(tvShowsRoot.Id, 0, int.MaxValue, out long totalChildren).ToList();
        foreach (var tvShowContent in tvShowContents)
        {
            _contentService.Delete(tvShowContent);
        }
        _logger.LogInformation("All TV Show content nodes have been deleted from backoffice.");
        return Ok("All TV Show content nodes have been deleted.");
    }
    
    private IContent? GetTvShowsRoot()
    {
        // Remember to use the correct alias
        var rootContent = _contentService.GetRootContent();
        return rootContent.FirstOrDefault(c => c.ContentType.Alias == "tvShows");
    }
}