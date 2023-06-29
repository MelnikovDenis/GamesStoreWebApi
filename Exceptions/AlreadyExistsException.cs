using System.Net;

namespace GamesStoreWebApi.Exceptions;

public class AlreadyExistsException : WebApiException
{
    public AlreadyExistsException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base(message, statusCode) { }
}
