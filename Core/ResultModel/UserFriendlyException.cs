using System;

namespace Basic.Core.ResultModel
{
    public class UserFriendlyException : Exception
    {
        public int BusinessCode { get; set; }
        public UserFriendlyException(int code, string message) : base(message)
        {
            BusinessCode = code;
        }
    }
}
