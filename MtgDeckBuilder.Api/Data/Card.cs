using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace MtgDeckBuilder.Api.Data;

[Owned]
public class Card
{
    [property: JsonPropertyName("name")]    
    public string Name { get; set; } = null!;
    
    [property: JsonPropertyName("id")]
    public string ScryfallId { get; set; } = null!;

    [property: JsonPropertyName("color_identity")]
    public string ColorIdentity { get; set; } = null!;
}
