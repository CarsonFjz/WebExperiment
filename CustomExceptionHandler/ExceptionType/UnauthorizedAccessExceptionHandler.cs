using Basic.Core.Exceptions;
using Basic.Core.ResultModel;

namespace Basic.CustomExceptionHandler.ExceptionType
{
    public class UnauthorizedAccessExceptionHandler
    {
        public static Error Get(UnauthorizedAccessCustomException ex)
        {
            return new Error(ex.Code, ex.Message);
        }
    }
}
