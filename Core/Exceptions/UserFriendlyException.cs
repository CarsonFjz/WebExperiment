using System;

namespace Basic.Core.Exceptions
{
    public class UserFriendlyException : Exception
    {
        public int Code { get; set; }
        public UserFriendlyException(int code, string message) : base(message)
        {
            Code = code;
        }
    }
}
