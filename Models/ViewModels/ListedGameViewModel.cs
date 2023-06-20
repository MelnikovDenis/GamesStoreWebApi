namespace GamesStoreWebApi.Models.ViewModels;

public class ListedGameViewModel
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string? PublisherName { get; private set; }
    public string? DeveloperName { get; private set; }
    public DateOnly ReleaseDate { get; private set; }
    public decimal CurrentPrice { get; private set; }
    public decimal? CurrentDiscount { get; private set; }
    public int KeyCount { get; private set; }
    public ListedGameViewModel(Guid id, 
        string title, 
        string? description, 
        string? publisherName, 
        string? developerName, 
        DateOnly releaseDate, 
        decimal currentPrice, 
        decimal? currentDiscount, 
        int keyCount)
    {
        Id = id;
        Title = title;
        Description = description;
        PublisherName = publisherName;
        DeveloperName = developerName;
        ReleaseDate = releaseDate;
        CurrentPrice = currentPrice;
        CurrentDiscount = currentDiscount;
        KeyCount = keyCount;
    }
}
