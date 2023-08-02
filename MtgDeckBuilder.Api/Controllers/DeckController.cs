using Microsoft.AspNetCore.Mvc;
using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;
using MtgDeckBuilder.Api.Services;

namespace MtgDeckBuilder.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DeckController : ControllerBase
{
    private readonly IDeckItemService _deckItemService;

    public DeckController(IDeckItemService deckItemService)
    {
        _deckItemService = deckItemService;
    }

    [HttpGet(Name = nameof(GetAllDecks))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<DeckItemDto>> GetAllDecks()
    {
        return Ok(_deckItemService.GetAll());
    }

    [HttpGet("{id:int}", Name = nameof(GetDeckById))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DeckItemDto> GetDeckById(int id)
    {
        var deck = _deckItemService.GetById(id);
        if (deck is null)
            return NotFound();
        return Ok(deck);
    }
}