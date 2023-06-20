namespace GamesStoreWebApi.Models.ViewModels;

public class ListGameViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? PublisherName { get; set; }
    public string? DeveloperName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public decimal CurrentPrice { get; set; }
    public decimal? CurrentDiscount { get; set; }
    public ListGameViewModel(Guid id, string title, string? description, string? publisherName, string? developerName, DateTime releaseDate, decimal currentPrice, decimal? currentDiscount)
    {
        Id = id;
        Title = title;
        Description = description;
        PublisherName = publisherName;
        DeveloperName = developerName;
        ReleaseDate = releaseDate;
        CurrentPrice = currentPrice;
        CurrentDiscount = currentDiscount;
    }
}
