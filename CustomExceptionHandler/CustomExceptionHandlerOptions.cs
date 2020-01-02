using Basic.Core.Exceptions;
using Basic.Core.ResultModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using Basic.CustomExceptionHandler.ExceptionType;

namespace Basic.CustomExceptionHandler
{
    public class CustomExceptionHandlerOptions
    {
        public Func<HttpContext, ILogger, Exception, Error> OnException { get; set; } =
            (context, logger, exception) =>
            {
                logger.LogError(exception.GetHashCode(),exception,"custom error");

                Error error;
                
                if (exception is UnauthorizedAccessCustomException)
                {
                    error = UnauthorizedAccessExceptionHandler.Get(exception as UnauthorizedAccessCustomException);
                }
                else if (exception is UserFriendlyException)
                {
                    error = UserFriendlyExceptionHandler.Get(exception as UserFriendlyException);
                }
                else
                {
                    error = new Error(500, exception.Message, exception.StackTrace);
                }

                return error;
            };

    }
}
