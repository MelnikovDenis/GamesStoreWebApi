namespace GamesStoreWebApi.Models.ViewModels;
public class DetailedGameViewModel
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public string? PublisherName { get; private set; }
    public string? PublisherDescription { get; private set; }
    public string? DeveloperName { get; private set; }
    public string? DeveloperDescription { get; private set; }
    public DateOnly ReleaseDate { get; private set; }
    public IEnumerable<PriceViewModel> PriceHistory { get; private set; }
    public IEnumerable<DiscountViewModel>? DiscountHistory { get; private set; }
    public int KeyCount { get; private set; }
    public DetailedGameViewModel(Guid id, 
        string title, 
        string? description, 
        string? publisherName, 
        string? publisherDescription, 
        string? developerName, 
        string? developerDescription, 
        DateOnly releaseDate, 
        IEnumerable<PriceViewModel> priceHistory, 
        IEnumerable<DiscountViewModel>? discountHistory, 
        int keyCount)
    {
        Id = id;
        Title = title;
        Description = description;
        PublisherName = publisherName;
        PublisherDescription = publisherDescription;
        DeveloperName = developerName;
        DeveloperDescription = developerDescription;
        ReleaseDate = releaseDate;
        PriceHistory = priceHistory;
        DiscountHistory = discountHistory;
        KeyCount = keyCount;
    }
}

public record class PriceViewModel(DateOnly StartDate, decimal Value);
public record class DiscountViewModel(DateOnly StartDate, DateOnly EndDate, decimal Percent);

