using System.Net;

namespace GamesStoreWebApi.Exceptions;
public class ItemNotFoundException : WebApiException
{
    public ItemNotFoundException()
        : base("Invalid id", HttpStatusCode.BadRequest) { }
}