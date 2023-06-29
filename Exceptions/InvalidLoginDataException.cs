using System.Net;

namespace GamesStoreWebApi.Exceptions;

public class InvalidLoginDataException : WebApiException
{
    public InvalidLoginDataException(string message = "Wrong email or password.", HttpStatusCode statusCode = HttpStatusCode.Unauthorized) 
        : base(message, statusCode) { }
}
