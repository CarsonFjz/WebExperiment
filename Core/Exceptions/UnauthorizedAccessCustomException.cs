using System;
using System.Net;

namespace Basic.Core.Exceptions
{
    public class UnauthorizedAccessCustomException : Exception
    {
        public int Code { get; set; }
        public UnauthorizedAccessCustomException(string message) : base(message)
        {
            Code = (int)HttpStatusCode.Unauthorized;
        }
    }
}
