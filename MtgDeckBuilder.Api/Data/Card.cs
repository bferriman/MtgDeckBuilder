using Microsoft.EntityFrameworkCore;

namespace MtgDeckBuilder.Api.Data;

[Owned]
public class Card
{
    // public int CardId { get; set; }
    
    public string Name { get; set; } = null!;
    public string ScryfallId { get; set; } = null!;
}
