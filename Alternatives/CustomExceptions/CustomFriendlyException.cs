using System;

namespace Alternatives.CustomExceptions
{
    public class CustomFriendlyException : Exception
    {
        public string FriendlyMessage => Message;

        public CustomFriendlyException(string friendlyMessage, Exception innerException = null)
            : base(friendlyMessage, innerException)
        {
        }
    }
}