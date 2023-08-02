using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;

namespace MtgDeckBuilder.Api.Services;

public class DbDeckItemService : IDeckItemService
{
    private readonly ILogger<DbDeckItemService> _logger;
    private readonly ApplicationContext _context;

    public DbDeckItemService(ILogger<DbDeckItemService> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IQueryable<DeckItemDto> GetAll()
    {
        _logger.LogInformation("Getting all decks");
        return _context.DeckItems.Select(deck => new DeckItemDto
        {
            Name = deck.Name,
            Commander = new CardDto
            {
                Name = deck.Commander.Name
            },
            NinetyNine = deck.NinetyNine.Select(card => new CardDto
            {
                Name = card.Name
            })
        });
    }

    public DeckItemDto? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public int? GetPriceById(int id)
    {
        throw new NotImplementedException();
    }

    public int? Create(string deckName, Card commander)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(int id, string deckName, Card commander)
    {
        throw new NotImplementedException();
    }

    public string? AddCard(string cardName)
    {
        throw new NotImplementedException();
    }

    public IQueryable<string> AddRandomCards(string query, int numCards)
    {
        throw new NotImplementedException();
    }

    public void RemoveCard(string cardName)
    {
        throw new NotImplementedException();
    }
}