using System;
using System.Net;

namespace Alternatives.CustomExceptions
{
    public class CustomApiException : Exception
    {
        public string FriendlyMessage => Message;
        public HttpStatusCode ReturnHttpStatusCode { get; }

        public CustomApiException(string friendlyMessage, HttpStatusCode returnStatusCode, Exception innerException = null)
            : base(friendlyMessage, innerException)
        {
            ReturnHttpStatusCode = returnStatusCode;
        }
    }
}