namespace GamesStoreWebApi.Models.ViewModels;

public class PageViewModel
{
    public int PageNumber { get; private set; }
    public int TotalPages { get; private set; }
    public PageViewModel(int count, int pageSize, int pageNumber = 1) 
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }
}