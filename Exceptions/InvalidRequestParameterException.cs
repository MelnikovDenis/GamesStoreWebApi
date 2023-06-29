using System.Net;

namespace GamesStoreWebApi.Exceptions;
public class InvalidRequestParameterException : WebApiException
{
    public InvalidRequestParameterException(string message = "Invalid request parameter.", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode) { }
}
