using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;

namespace MtgDeckBuilder.Api.Services;

public interface IDeckItemService
{
    IQueryable<DeckItemDto> GetAll();
    DeckItemDto? GetById(int id);
    int? GetPriceById(int id);
    Task<int?> Create(string deckName, string commanderName);
    void Delete(int id);
    Task Update(int id, string deckName, string commanderName);
    string? AddCard(string cardName);
    IQueryable<string> AddRandomCards(string query, int numCards);
    void RemoveCard(string cardName);
}