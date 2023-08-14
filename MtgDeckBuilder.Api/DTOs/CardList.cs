using System.Text.Json.Serialization;
using MtgDeckBuilder.Api.Data;

namespace MtgDeckBuilder.Api.DTOs;

public class CardList
{
    [property: JsonPropertyName("data")]
    public List<Card> Cards { get; set; }

}