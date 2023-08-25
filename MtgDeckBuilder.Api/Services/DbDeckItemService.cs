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
            Id = deck.Id,
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
                Id = deck.Id,
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

    public async Task<decimal?> GetPriceById(int id)
    {
        var target = _context.DeckItems.SingleOrDefault(item => item.Id == id);
        if (target is null)
        {
            _logger.LogWarning("Invalid deck id in price request: {DeckId}", id);
            return null;
        }
        var totalPrice = 0.00m;
        
        // get and add price of commander to total
        var cardPrice = await _scryfall.GetCardPriceDataById(target.Commander.ScryfallId, _logger);
        if (cardPrice.HasValue) totalPrice += cardPrice.Value;
        Thread.Sleep(100);
        
        // get and add price of all cards in the ninety-nine to total
        foreach (var card in target.NinetyNine)
        {
            cardPrice = await _scryfall.GetCardPriceDataById(card.ScryfallId, _logger);
            if (cardPrice.HasValue) totalPrice += cardPrice.Value;
            Thread.Sleep(100);
        }

        return totalPrice;
    }

    public async Task<int?> Create(string deckName, string commanderName)
    {
        var commander = await _scryfall.GetCardByName(commanderName, _logger);
        if (commander is null) return null;
        var deckItem = new DeckItem
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
        var target = _context.DeckItems.SingleOrDefault(item => item.Id == id);
        if (target is null)
        {
            _logger.LogWarning("Invalid deck id in delete request: {DeckId}", id);
            return;
        }

        _context.DeckItems.Remove(target);
        _context.SaveChanges();
    }

    public async Task Update(int id, string deckName, string commanderName)
    {
        var target = _context.DeckItems.SingleOrDefault(item => item.Id == id);
        if (target is null)
        {
            _logger.LogWarning("Invalid deck id in update request: {DeckId}", id);
            return;
        }

        if (target.Commander.Name != commanderName)
        {
            var commander = await _scryfall.GetCardByName(commanderName, _logger);
            if (commander is null)
            {
                _logger.LogWarning("Aborting Update: Commander lookup failed");
                return;
            }
            target.Commander = commander;
        }

        target.Name = deckName;
        _context.SaveChanges();
    }

    public async Task<string?> AddCard(int deckId, string cardName)
    {
        var target = _context.DeckItems.SingleOrDefault(item => item.Id == deckId);
        if (target is null)
        {
            _logger.LogWarning("Invalid deck id in add card request: {DeckId}", deckId);
            return null;
        }
        var card = await _scryfall.GetCardByName(cardName, _logger);
        if (card is null)
        {
            _logger.LogWarning("Aborting Add Card Operation: Card lookup failed");
            return null;
        }

        if (target.NinetyNine.Any(c => c.Name == card.Name))
        {
            _logger.LogWarning("Aborting Add Card Operation: Card already in deck");
            return null;
        }
        target.NinetyNine.Add(card);
        _context.SaveChanges();
        return card.Name;
    }

    public IQueryable<CardDto> AddRandomCards(int deckId, string query, int numCards)
    {
        throw new NotImplementedException();
    }

    public int? RemoveCard(int deckId, string cardName)
    {
        var target = _context.DeckItems.SingleOrDefault(item => item.Id == deckId);
        if (target is null)
        {
            _logger.LogWarning("Invalid deck id in remove card request: {DeckId}", deckId);
            return null;
        }

        var removedCount = target.NinetyNine.RemoveAll(c => c.Name == cardName);

        if (removedCount > 0) _context.SaveChanges();
        return removedCount;
    }
}