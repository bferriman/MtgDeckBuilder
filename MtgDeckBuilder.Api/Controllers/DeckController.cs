using Microsoft.AspNetCore.Mvc;
using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;
using MtgDeckBuilder.Api.Models;
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

    [HttpPost(Name = nameof(CreateDeckItem))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDeckItem([FromBody] CreateDeckItemViewModel model)
    {
        var createdId = await _deckItemService.Create(model.DeckName, model.CommanderName);
        if (createdId is not null)
            return CreatedAtRoute(nameof(GetDeckById), new { id = createdId!.Value }, null);
        ModelState.AddModelError("CommanderName", "Provided commander not found");
        return BadRequest(ModelState);
    }

    [HttpDelete(Name = nameof(DeleteDeckItem))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult DeleteDeckItem(int id)
    {
        _deckItemService.Delete(id);
        return NoContent();
    }

    [HttpPut("{id:int}", Name = nameof(UpdateDeckItem))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateDeckItem(int id, [FromBody] UpdateDeckItemViewModel model)
    {
        await _deckItemService.Update(id, model.DeckName, model.CommanderName);
        return NoContent();
    }

    [HttpPut("add_card/{id:int}", Name = nameof(AddCardToDeck))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AddCardToDeck(int id, string cardName)
    {
        var result = await _deckItemService.AddCard(id, cardName);
        if (result is null) return NotFound();
        return NoContent();
    }
}