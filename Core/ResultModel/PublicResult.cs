using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Core.ResultModel
{
    public class PublicResult<T> where T : class
    {
        public PublicResult(T result)
        {
            Result = result;
        }

        public PublicResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public PublicResult(int code, string message, T result) : this(result)
        {
            Code = code;
            Message = message;
        }
        public int Code { get; set; }
        public string Message { get; set; }

        public T Result { get; set; }
    }
}
