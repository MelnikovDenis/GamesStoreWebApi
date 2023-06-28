using System.Net;

namespace GamesStoreWebApi.Exceptions;
public class ItemNotFoundException : WebApiException
{
    public ItemNotFoundException()
        : base("Item not found by id.", HttpStatusCode.BadRequest) { }
}