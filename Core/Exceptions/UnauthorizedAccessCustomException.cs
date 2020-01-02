using System;
using System.Net;

namespace Basic.Core.Exceptions
{
    public class UnauthorizedAccessCustomException : BaseCustomException
    {
        public UnauthorizedAccessCustomException(string message) : base((int)HttpStatusCode.Unauthorized, message,null)
        {

        }
    }
}
