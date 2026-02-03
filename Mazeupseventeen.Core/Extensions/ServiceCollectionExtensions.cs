using Mazeupseventeen.Core.Interfaces;
using Mazeupseventeen.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Mazeupseventeen.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTvmazeServices(this IServiceCollection services)
    {
        services.AddHttpClient<ITvMazeApiService, TvMazeApiApiService>(client =>
        {
            client.BaseAddress = new Uri("https://api.tvmaze.com/");
        });
        
        services.AddScoped<ITvShowImportService, TvShowImportService>();
        services.AddScoped<IContentManagementService, ContentManagementService>();
        return services;
    }
}