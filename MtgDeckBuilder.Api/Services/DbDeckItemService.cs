using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;

namespace MtgDeckBuilder.Api.Services;

public class DbDeckItemService : IDeckItemService
{
    private readonly ILogger<DbDeckItemService> _logger;
    private readonly ApplicationContext _context;
    private readonly ScryfallService _scryfall;

    public DbDeckItemService(ILogger<DbDeckItemService> logger, ApplicationContext context)
    {
        _logger = logger;
        _context = context;
        _scryfall = new ScryfallService();
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
        var target = _context.DeckItems
            .Where(deck => deck.Id == id)
            .Select(deck => new DeckItemDto
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
            }).SingleOrDefault();
        if (target is null)
        {
            _logger.LogWarning("Request for deck with id {DeckId} failed - Id not found", id);
        }

        return target;
    }

    public int? GetPriceById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int?> Create(string deckName, string commanderName)
    {
        Card? commander = await _scryfall.GetCardByName(commanderName, _logger);
        if (commander is null) return null;
        DeckItem deckItem = new DeckItem
        {
            Name = deckName,
            Commander = commander,
            NinetyNine = new List<Card>()
        };
        _context.DeckItems.Add(deckItem);
        _context.SaveChanges();
        return deckItem.Id;
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