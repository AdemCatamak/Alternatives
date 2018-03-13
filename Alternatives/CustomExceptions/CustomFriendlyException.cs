using System;

namespace Alternatives.CustomExceptions
{
    public class CustomFriendlyException : Exception
    {
        public string FriendlyMessage => Message;
        public string[] Tags { get; }

        public CustomFriendlyException(string friendlyMessage) : base(friendlyMessage, null)
        { }

        public CustomFriendlyException(string friendlyMessage, Exception innerException)
            : base(friendlyMessage, innerException)
        { }

        public CustomFriendlyException(string friendlyMessage, params string[] tag) : base(friendlyMessage, null)
        {
            Tags = tag;
        }

        public CustomFriendlyException(string friendlyMessage, Exception innerException ,params string[] tag) : base(friendlyMessage, innerException)
        {
            Tags = tag;
        }

    }
}