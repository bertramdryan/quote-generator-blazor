using QuoteGenerator.Models;

namespace QuoteGenerator.Services.Interfaces
{
    public interface IQuoteService
    {
        event Action? OnChange;
        bool Ready { get; set; }
        QuoteModel SelectedQuote { get; set; }
        Task FetchQuotes();

        Task SelectRandomQuote();
    }
}