using System.Net;

namespace GamesStoreWebApi.Exceptions
{
    public class InvalidRequestParameterException : WebApiException
    {
        public InvalidRequestParameterException()
            : base("Invalid request parameter.", HttpStatusCode.BadRequest) { }
        public InvalidRequestParameterException(string message)
            : base(message, HttpStatusCode.BadRequest) { }
    }
}
