using System;
using System.Net;

namespace Alternatives.CustomExceptions
{
    public class CustomApiException : Exception
    {
        public string FriendlyMessage => Message;
        public HttpStatusCode ReturnHttpStatusCode { get; }
        public string[] Tags { get; }

        public CustomApiException(string friendlyMessage, HttpStatusCode returnStatusCode)
           : base(friendlyMessage, null)
        {
            ReturnHttpStatusCode = returnStatusCode;
        }

        public CustomApiException(string friendlyMessage, HttpStatusCode returnStatusCode, Exception innerException)
            : base(friendlyMessage, innerException)
        {
            ReturnHttpStatusCode = returnStatusCode;
        }

        public CustomApiException(string friendlyMessage, HttpStatusCode returnStatusCode, params string[] tags)
            : base(friendlyMessage, null)
        {
            ReturnHttpStatusCode = returnStatusCode;
            Tags = tags;
        }

        public CustomApiException(string friendlyMessage, HttpStatusCode returnStatusCode, Exception innerException, params string[] tags)
            : base(friendlyMessage, innerException)
        {
            ReturnHttpStatusCode = returnStatusCode;
            Tags = tags;
        }
    }
}