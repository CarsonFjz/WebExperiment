using JwtSecurityTokenExtension.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace JwtSecurityTokenExtension.Implementation
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly IMemoryCache _memoryCache;

        public RefreshTokenStore(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public RefreshTokenModel Get(string refreshToken)
        {
            return _memoryCache.Get<RefreshTokenModel>(refreshToken);
        }

        public void Set(string refreshToken, RefreshTokenModel model, TimeSpan expirationTime)
        {
            _memoryCache.Set<RefreshTokenModel>(refreshToken, model, expirationTime);
        }
    }
}
