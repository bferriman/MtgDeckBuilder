using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;

namespace MtgDeckBuilder.Api.Services;

public interface IDeckItemService
{
    IQueryable<DeckItemDto> GetAll();
    DeckItemDto? GetById(int id);
    Task<decimal?> GetPriceById(int id);
    Task<int?> Create(string deckName, string commanderName);
    void Delete(int id);
    Task Update(int id, string deckName, string commanderName);
    Task<string?> AddCard(int deckId, string cardName);
    IQueryable<string> AddRandomCards(int deckId, string query, int numCards);
    int? RemoveCard(int deckId, string cardName);
}