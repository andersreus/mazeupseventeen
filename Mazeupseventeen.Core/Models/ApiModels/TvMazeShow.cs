using System.Text.Json.Serialization;

namespace Mazeupseventeen.Core.Models.ApiModels;

public class TvMazeShow
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    /*[JsonPropertyName("genres")]
    public string[] Genres { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("premiered")]
    public DateOnly Premiered { get; set; }
    
    [JsonPropertyName("ended")]
    public DateOnly Ended { get; set; }
    
    [JsonPropertyName("network.name")]
    public string NetworkName { get; set; }
    
    [JsonPropertyName("rating.average")]
    public double ExternalRating { get; set; }
    
    [JsonPropertyName("externals.imdb")]
    public string ImdbId { get; set; }
    
    [JsonPropertyName("image.original")]
    public string ImageUrl { get; set; }*/
    
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}