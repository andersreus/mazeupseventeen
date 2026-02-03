using Mazeupseventeen.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mazeupseventeen.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImportController : Controller
{
    private readonly ILogger<ImportController> _logger;
    private readonly ITvShowImportService _tvShowImportService;
    
    public ImportController(ILogger<ImportController> logger, ITvShowImportService tvShowImportService)
    {
        _logger = logger;
        _tvShowImportService = tvShowImportService;
    }

    [HttpPost("import")]
    public async Task<IActionResult> StartImport()
    {
        _logger.LogInformation("Manual import of tvshows has been triggered");
        
        await _tvShowImportService.ImportAllShowsAsync();
        
        return Ok();
    }
    
    [HttpGet("test")]
    public async Task<IActionResult> test()
    {
        return Ok("The API is working!");
    }
}