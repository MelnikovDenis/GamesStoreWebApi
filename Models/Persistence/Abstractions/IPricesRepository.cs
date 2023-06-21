using GamesStoreWebApi.Models.Entities;

namespace GamesStoreWebApi.Models.Persistence.Abstractions;
public interface IPricesRepository
{
    public void CreatePrice(Price price);
}