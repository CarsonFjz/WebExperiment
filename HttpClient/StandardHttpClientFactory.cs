using Basic.Core.ResultModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Basic.HttpClient
{
    public class StandardHttpClientFactory : IHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandardHttpClientFactory(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public virtual HttpRequestMessage InitHttpRequestMessage()
        {
            return new HttpRequestMessage();
        }
        public void HandlerErrorResult(HttpResponseMessage responseMessage)
        {
            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpClientException(
                    responseMessage.StatusCode,
                    responseMessage.Content,
                    responseMessage.RequestMessage);
            }
        }

        #region Get

        public async Task<string> GetAsync(
            string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = GetHttpRequestMessage(
                HttpMethod.Get,
                uri,
                authorizationToken,
                authorizationMethod,
                null);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            HandlerErrorResult(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<T> GetAsync<T>(
            string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer")
        {
            var resultStr = await GetAsync(
                uri,
                authorizationToken,
                authorizationMethod);

            return ConvertJson<T>(resultStr);
        }

        #endregion

        #region Post

        public async Task<HttpResponseMessage> PostAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(
                HttpMethod.Post,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
        }

        public async Task<T> PostAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var result = await DoPostPutAsync(HttpMethod.Post,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
            var resultStr = await result.Content.ReadAsStringAsync();
            return ConvertJson<T>(resultStr);
        }

        #endregion

        #region Put

        public async Task<T> PutAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var result = await DoPostPutAsync(
                HttpMethod.Put,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
            var resultStr = await result.Content.ReadAsStringAsync();
            return ConvertJson<T>(resultStr);
        }

        public async Task<HttpResponseMessage> PutAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(
                HttpMethod.Put,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
        }

        #endregion

        #region Delete

        public async Task<T> DeleteAsync<T>(
            string uri,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var result = await DeleteAsync(
                uri,
                authorizationToken,
                requestId,
                authorizationMethod);
            var resultStr = await result.Content.ReadAsStringAsync();
            return ConvertJson<T>(resultStr);
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string uri,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = GetHttpRequestMessage(
                HttpMethod.Delete,
                uri,
                authorizationToken,
                authorizationMethod,
                requestId);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            HandlerErrorResult(response);

            return response;
        }

        #region  删除（以对象参数post方式）
        /// <summary>
        ///  删除（以对象参数post方式）
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(
                HttpMethod.Delete,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
        }
        #endregion

        #region 删除（以对象参数post方式）
        /// <summary>
        ///  删除（以对象参数post方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">控制器URL</param>
        /// <param name="item">对象参数</param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var result = await DoPostPutAsync(HttpMethod.Delete,
                uri,
                item,
                mediaType,
                authorizationToken,
                requestId,
                authorizationMethod);
            var resultStr = await result.Content.ReadAsStringAsync();
            return ConvertJson<T>(resultStr);
        }
        #endregion

        #endregion

        /// <summary>
        /// 执行更新请求
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        private async Task<HttpResponseMessage> DoPostPutAsync(
            HttpMethod method,
            string uri,
            object item,
            string mediaType,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer")
        {
            var requestMessage = GetHttpRequestMessage(
                method,
                uri,
                authorizationToken,
                authorizationMethod,
                requestId);

            requestMessage.Headers.Add("Accept", mediaType);

            if (mediaType == "application/x-www-form-urlencoded")
                requestMessage.Content = FormPost(item);
            else if (mediaType == "application/json")
                requestMessage.Content = JsonPost(item);
            else
                throw new Exception("只能表单或json提交");
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            HandlerErrorResult(response);

            return response;
        }

        /// <summary>
        /// 获取http请求消息体
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="authorizationMethod"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public HttpRequestMessage GetHttpRequestMessage(
            HttpMethod method,
            string uri,
            string authorizationToken = null,
            string authorizationMethod = null,
            string requestId = null)
        {
            var requestMessage = InitHttpRequestMessage();

            requestMessage.Method = method;

            requestMessage.RequestUri = new Uri(uri);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
                requestMessage.Headers.Add("x-requestid", requestId);

            return requestMessage;
        }

        #region 封装不同提交方式

        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        private HttpContent FormPost(object item)
        {
            var fromData = ToListKeyValue(item);

            var content = new FormUrlEncodedContent(fromData);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            return content;
        }

        /// <summary>
        /// json提交
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private HttpContent JsonPost(object item)
        {
            var jsonStr = "";
            if (item != null)
                jsonStr = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonStr, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }

        /// <summary>
        /// 将对象属性转换为key-value对
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<KeyValuePair<string, string>> ToListKeyValue(object data)
        {
            var map = new List<KeyValuePair<string, string>>();
            if (data == null)
                return map;
            var t = data.GetType();
            if (t.Name == "Dictionary`2")
            {
                var dic = (Dictionary<string, string>)data;
                foreach (var p in dic)
                {
                    map.Add(
                        new KeyValuePair<string, string>(
                            p.Key, p.Value
                        ));
                }

                return map;
            }

            var pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var p in pi)
            {
                var mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(new KeyValuePair<string, string>(
                        p.Name, mi.Invoke(data, new object[] { }).ToString()
                    ));
                }
            }

            return map;
        }

        #endregion

        #region 序列化

        private T ConvertJson<T>(string value)
        {
            if (typeof(T) == typeof(string))
                return (T)(object)value;

            if (string.IsNullOrEmpty(value))
                return default;
            return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings());
        }

        #endregion
    }
}

