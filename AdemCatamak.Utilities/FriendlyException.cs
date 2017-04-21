using System;

namespace AdemCatamak.Utilities
{
    public class FriendlyException : Exception
    {
        public string FriendlyMessage { get; }
        public bool IsFatal { get; }

        public FriendlyException(string friendlyMessage, bool isFatal = false)
        {
            FriendlyMessage = friendlyMessage;
            IsFatal = isFatal;
        }

        public FriendlyException(string friendlyMessage, Exception ex, bool isFatal = false) : base(ex.Message, ex)
        {
            FriendlyMessage = friendlyMessage;
            IsFatal = isFatal;
        }
    }
}