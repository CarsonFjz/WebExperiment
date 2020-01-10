using System;

namespace Basic.Core.ResultModel
{
    public class UserFriendlyException : Exception
    {
        public UserFriendlyException(string message) : base(message)
        {

        }

        public int Code { get; set; }
    }
}
