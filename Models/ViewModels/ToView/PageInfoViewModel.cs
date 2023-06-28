using GamesStoreWebApi.Exceptions;

namespace GamesStoreWebApi.Models.ViewModels.ToView;

public record class PageInfoViewModel
{
    public int PageNumber { get; init; }
    public int TotalPages { get; init; }
    public PageInfoViewModel(int count, int pageSize, int pageNumber = 1)
    {
        if (pageSize <= 0 || pageNumber <= 0 || pageNumber * pageSize - count > pageSize)
            throw new InvalidRequestParameterException("Invalid pagination info.");
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}