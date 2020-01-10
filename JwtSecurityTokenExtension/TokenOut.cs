using System;

namespace Basic.JwtSecurityTokenExtension
{
    public class TokenOut
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// token过期时间
        /// </summary>
        public DateTime TokenExpires { get; set; }
    }
}
