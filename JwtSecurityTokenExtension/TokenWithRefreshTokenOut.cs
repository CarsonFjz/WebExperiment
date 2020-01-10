using System;

namespace Basic.JwtSecurityTokenExtension
{
    public class TokenWithRefreshTokenOut : TokenOut
    {
        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// refresh token过期时间
        /// </summary>
        public DateTime RefreshTokenExpires { get; set; }
    }
}
