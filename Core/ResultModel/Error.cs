using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Core.ResultModel
{
    public class Error
    {
        public Error(int code)
        {
            Code = code;
        }
        public Error(int code, string message): this(code)
        {
            Message = message;
        }

        public Error(int code, string message, string detail) : this(code, message)
        {
            Detail = detail;
        }
        public int Code { get; set; }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
