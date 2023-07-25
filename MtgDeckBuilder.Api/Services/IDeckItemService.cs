using MtgDeckBuilder.Api.Data;

namespace MtgDeckBuilder.Api.Services;

public interface IDeckItemService
{
    IQueryable<DeckItem> GetAll();
    DeckItem? GetById(int id);
    int? GetPriceById(int id);
    int? Create(string deckName, Card commander);
    void Delete(int id);
    void Update(int id, string deckName, Card commander);
    string? AddCard(string cardName);
    IQueryable<string> AddRandomCards(string query, int numCards);
    void RemoveCard(string cardName);
}