namespace Basic.JwtSecurityTokenExtension
{
    public class JwtOption
    {
        /// <summary>
        /// 发布方
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 接受方
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 发布方密钥
        /// </summary>
        public string IssuerSigningKey { get; set; }

        /// <summary>
        /// Token过期时间（分）
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; } = 15;

        /// <summary>
        /// 刷新Token过期时间（分）
        /// </summary>
        public int RefreshTokenExpiresMinutes { get; set; } = 60;

        /// <summary>
        /// 是否使用刷新Token
        /// </summary>
        public bool IsUseRefreshToken { get; set; } = false;
    }
}
