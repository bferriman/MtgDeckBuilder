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
    public ActionResult<IEnumerable<DeckItemDto>> GetAllDecks()
    {
        return Ok(_deckItemService.GetAll());
    }
}