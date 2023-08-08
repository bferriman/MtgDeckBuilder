using System.ComponentModel.DataAnnotations;

namespace MtgDeckBuilder.Api.Data;

public class DeckItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Card Commander { get; set; }
    public List<Card> NinetyNine { get; set; }
}