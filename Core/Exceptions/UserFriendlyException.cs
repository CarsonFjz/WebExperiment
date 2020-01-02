using System;

namespace Basic.Core.Exceptions
{
    public class UserFriendlyException : BaseCustomException
    {
        public UserFriendlyException(int code, string message) : base(code, message, null)
        {
            Code = code;
        }
    }
}
