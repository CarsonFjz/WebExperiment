using System;
using System.Collections.Generic;
using System.Text;

namespace Basic.Core.ResultModel
{
    public class Result<T> where T : class
    {
        public Result(bool success)
        {
            Success = success;
        }

        public Result(Error error) : this(false)
        {
            Error = error;
        }

        public Result(T resultContent) : this(true)
        {
            ResultContent = resultContent;
        }

        public bool Success { get; set; } = false;

        public Error Error { get; set; }

        public T ResultContent { get; set; }
    }
}
