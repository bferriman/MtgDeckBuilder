namespace MtgDeckBuilder.Api.DTOs;

public class DeckItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public CardDto Commander { get; set; }
    public IEnumerable<CardDto> NinetyNine { get; set; }
}