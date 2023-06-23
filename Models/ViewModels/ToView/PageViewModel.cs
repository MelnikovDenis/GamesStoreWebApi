namespace GamesStoreWebApi.Models.ViewModels.ToView;

public record class PageViewModel
{
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }
    public PageViewModel(int count, int pageSize, int pageNumber = 1)
    {
        if (pageSize <= 0 || pageNumber <= 0 || pageNumber * pageSize - count > pageSize)
            throw new ArgumentOutOfRangeException("Invalid pagination info");
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}