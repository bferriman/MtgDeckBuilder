using System.Net.Http.Headers;
using System.Text.Json;
using MtgDeckBuilder.Api.Data;

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
            var responseStream = await _client.GetStreamAsync($"https://api.scryfall.com/cards/named?fuzzy={name}");

            Card? card = await JsonSerializer.DeserializeAsync<Card>(responseStream);
            return card;
        }

        catch (HttpRequestException)
        {
            logger.LogWarning("Error during deck creation: Commander with name '{CommanderName}' was not found", name);
            return null;
        }
    }
}