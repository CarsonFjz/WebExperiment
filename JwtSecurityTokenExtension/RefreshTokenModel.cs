using System;
using System.Collections.Generic;

namespace JwtSecurityTokenExtension
{
    public class RefreshTokenModel
    {
        /// <summary>
        /// 存储的用户信息
        /// </summary>
        public Dictionary<string, string> ClaimsContent { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
