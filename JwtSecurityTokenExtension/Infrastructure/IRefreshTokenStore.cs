using System;
using System.Collections.Generic;

namespace JwtSecurityTokenExtension.Infrastructure
{
    public interface IRefreshTokenStore
    {
        /// <summary>
        /// 根据refreshToken获取用户信息
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        RefreshTokenModel Get(string refreshToken);
        /// <summary>
        /// 设置refreshToken
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="model"></param>
        /// <param name="expirationTime"></param>
        void Set(string refreshToken, RefreshTokenModel model, TimeSpan expirationTime);
    }
}
