using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basic.CustomExceptionHandler
{
    public static class HttpContextExtension
    {
        public static async Task HttpWriteAsync(this HttpContext context, int httpStatusCode, object result)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
