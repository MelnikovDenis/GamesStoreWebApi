using System.Net;

namespace GamesStoreWebApi.Exceptions;
public class ItemNotFoundException : WebApiException
{
    public ItemNotFoundException(string message = "Item not found by id.", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode) { }
}