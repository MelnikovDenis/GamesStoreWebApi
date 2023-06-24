using System.Net;

namespace GamesStoreWebApi.Exceptions;
public class WebApiException : Exception
{
    public HttpStatusCode StatusCode { get; private set; }
    public WebApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }
}