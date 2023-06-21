using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence.Abstractions;
public interface ICompaniesRepository
{
    public IQueryable<Company> GetCompanies();
}