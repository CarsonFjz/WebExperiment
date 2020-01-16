using System.Net.Http;
using System.Threading.Tasks;

namespace Basic.HttpClient
{
    public interface IHttpClient
    {
        #region Get

        Task<string> GetAsync(
            string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        Task<T> GetAsync<T>(
            string uri,
            string authorizationToken = null,
            string authorizationMethod = "Bearer");

        #endregion

        #region Post

        Task<HttpResponseMessage> PostAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        /// <summary>
        /// Post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<T> PostAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        #endregion

        #region Delete
        /// <summary>
        /// 删除请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(
            string uri,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        /// <summary>
        /// 删除请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<T> DeleteAsync<T>(
            string uri,
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        #region 删除（以对象参数post方式）
        /// <summary>
        /// 删除（以对象参数post方式）
        /// </summary>
        /// <param name="uri">控制器URL</param>
        /// <param name="item">对象参数</param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");
        #endregion

        #region 删除（以对象参数post方式）
        /// <summary>
        /// 删除（以对象参数post方式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">控制器URL</param>
        /// <param name="item">对象参数</param>
        /// <param name="mediaType">默认application json方式</param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod">默认Bearer</param>
        /// <returns></returns>
        Task<T> DeleteAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");
        #endregion

        #endregion

        #region Put

        /// <summary>
        /// 更新请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        /// <summary>
        /// 更新请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="item"></param>
        /// <param name="mediaType"></param>
        /// <param name="authorizationToken"></param>
        /// <param name="requestId"></param>
        /// <param name="authorizationMethod"></param>
        /// <returns></returns>
        Task<T> PutAsync<T>(
            string uri,
            object item,
            string mediaType = "application/json",
            string authorizationToken = null,
            string requestId = null,
            string authorizationMethod = "Bearer");

        #endregion
    }
}
