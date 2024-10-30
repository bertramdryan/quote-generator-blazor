using Microsoft.AspNetCore.Components;
using QuoteGenerator.Models;
using QuoteGenerator.Services.Interfaces;
using System.Xml;

namespace QuoteGenerator.Components.QuoteComponent;

public partial class QuoteComponent : IDisposable
{
    [Inject]
    private IQuoteService QuoteService { get; set; }
    [Inject]
    private NavigationManager NavigationManager { get; set; }

    public void Dispose()
    {
        QuoteService.OnChange -= StateHasChanged;
        QuoteService.SelectedQuote = new QuoteModel();
    }

    protected override void OnInitialized()
    {
        QuoteService.OnChange += StateHasChanged;
    }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        { 
            await QuoteService.FetchQuotes();
            await QuoteService.SelectRandomQuote();
            StateHasChanged();
        }
    }

    private async Task HandleRandomQuoteClick()
    {
        await QuoteService.SelectRandomQuote();
        StateHasChanged();
    }
    private void HandlePostToX()
    {
        string xUrl = $"https://x.com/intent/post?text={QuoteService.SelectedQuote.Text} -{QuoteService.SelectedQuote.Author}";
       
        NavigationManager.NavigateTo(xUrl, true);
    }
}
