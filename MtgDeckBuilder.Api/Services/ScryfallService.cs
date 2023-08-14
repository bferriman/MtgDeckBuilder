using System.Net.Http.Headers;
using System.Text.Json;
using MtgDeckBuilder.Api.Data;
using MtgDeckBuilder.Api.DTOs;

namespace MtgDeckBuilder.Api.Services;

public class ScryfallService
{
    private HttpClient _client;

    public ScryfallService()
    {
        _client = new HttpClient();
    }

    public async Task<Card?> GetCardByName(string name, ILogger<DbDeckItemService> logger)
    {
        try
        {
            // var responseStream = await _client.GetStreamAsync($"https://api.scryfall.com/cards/named?fuzzy={name}");
            var url =
                $"https://api.scryfall.com/cards/search?order=name&unique=prints&q=!%22{name}%22+%28game%3Apaper%29+legal%3Acommander+%28lang%3Aen%29";
            var responseStream = await _client.GetStreamAsync(url);

            var cardList = await JsonSerializer.DeserializeAsync<CardList>(responseStream);
            return cardList.Cards[0];
        }

        catch (HttpRequestException)
        {
            logger.LogWarning("Error during card lookup: Card with name '{CardName}' was not found", name);
            return null;
        }
    }

    public async Task<decimal?> GetCardPriceDataById(string id, ILogger<DbDeckItemService> logger)
    {
        try
        {
            var responseStream = await _client.GetStreamAsync($"https://api.scryfall.com/cards/{id}");

            var cardPricesData = await JsonSerializer.DeserializeAsync<CardPrices>(responseStream);
            if (decimal.TryParse(cardPricesData.Prices["usd"], out decimal price))
            {
                return price;
            }
            return null;
        }
        catch (HttpRequestException)
        {
            logger.LogWarning("Error during card price lookup: Card with id '{CardId}' was not found", id);
            return null;
        }
    }
}