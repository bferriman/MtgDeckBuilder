namespace MtgDeckBuilder.Api.Data;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ScryfallId { get; set; } = null!;
}
