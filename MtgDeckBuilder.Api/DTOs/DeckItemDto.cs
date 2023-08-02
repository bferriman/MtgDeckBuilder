namespace MtgDeckBuilder.Api.DTOs;

public class DeckItemDto
{
    public string Name { get; set; } = null!;
    public CardDto Commander { get; set; }
    public IEnumerable<CardDto> NinetyNine { get; set; }
}