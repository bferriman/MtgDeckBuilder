using System.ComponentModel.DataAnnotations;

namespace MtgDeckBuilder.Api.Models;

public class UpdateDeckItemViewModel
{
    [Required]
    public string DeckName { get; set; } = null!;
    [Required]
    public string CommanderName { get; set; } = null!;
}