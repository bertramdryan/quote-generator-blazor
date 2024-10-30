using QuoteGenerator.Models;
using QuoteGenerator.Services.Interfaces;

namespace QuoteGenerator.Services;

public class QuoteService : IQuoteService
{
    private readonly HttpClient _httpClient;

    public QuoteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public bool Ready { get; set; } = false;

    public QuoteModel SelectedQuote { get; set; }

    public async Task SelectRandomQuote()
    {
        Random random = new();
        int randomIndex = random.Next(_quotes.Count);
        SelectedQuote = _quotes[randomIndex];
    }

    private List<QuoteModel> _quotes = [];
    public event Action? OnChange;


    public async Task FetchQuotes()
    {
        try
        {
            string url = "https://jacintodesign.github.io/quotes-api/data/quotes.json";
            var quotes = await _httpClient.GetFromJsonAsync<List<QuoteModel>>(url);

            if (quotes != null && quotes?.Count > 0)
            {
                _quotes = quotes;
                Ready = true;
                NotifyStateHasChanged();
            }
        }
        catch (Exception ex)
        {
            // Throw excepion
        }
    }


    private void NotifyStateHasChanged() => OnChange?.Invoke();

}
