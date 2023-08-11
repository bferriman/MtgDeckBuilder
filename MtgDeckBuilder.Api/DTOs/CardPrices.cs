using System.Text.Json.Serialization;

namespace MtgDeckBuilder.Api.DTOs;

public class CardPrices
{
    [property: JsonPropertyName("prices")]
    public Dictionary<string, string?> Prices { get; set; }
}