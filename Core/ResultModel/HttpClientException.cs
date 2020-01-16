using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Basic.Core.ResultModel
{
    public class HttpClientException : Exception
    {
        /// <summary>
        /// http异常处理
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="content"></param>
        /// <param name="requestMessage"></param>
        public HttpClientException(HttpStatusCode httpStatusCode, object content, HttpRequestMessage requestMessage)
        {
            HttpStatusCode = httpStatusCode;
            Url = requestMessage.RequestUri.ToString();
            HttpMethod = requestMessage.Method;
            Content = JsonConvert.SerializeObject(content);

            foreach (var item in requestMessage.Headers)
            {
                Header.Add(item.Key, item.Value);
            }
        }
        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }
        /// <summary>
        /// http 方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; }
        /// <summary>
        /// 头
        /// </summary>
        public Dictionary<string, object> Header { get; set; } = new Dictionary<string, object>();
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}