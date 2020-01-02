using System;

namespace Basic.Core.Exceptions
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException(int code, string message, string detail)
        {
            Code = code;
            CustomMessage = message;
            Detail = detail;
        }
        public int Code { get; set; }
        public string CustomMessage { get; set; }
        public string Detail { get; set; }
    }
}
