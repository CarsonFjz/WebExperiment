using Basic.Core.Exceptions;
using Basic.Core.ResultModel;

namespace Basic.CustomExceptionHandler.ExceptionType
{
    public class UserFriendlyExceptionHandler
    {
        public static Error Get(UserFriendlyException ex)
        {
            return new Error(ex.Code, ex.CustomMessage);
        }
    }
}
