namespace Basic.Core
{
    public class AuthenticatorKey
    {
        /// <summary>
        /// 权限
        /// </summary>
        public const string Permission = "per";
        /// <summary>
        /// 角色
        /// </summary>
        public const string Role = "rol";

        /// <summary>
        /// 用户
        /// </summary>
        public const string UserId = "uid";

        /// <summary>
        /// JWT接收对象
        /// </summary>
        public const string Audience = "aud";

        /// <summary>
        /// JWT签发主体
        /// </summary>
        public const string Issued = "iss";

        /// <summary>
        /// JWT过期时间
        /// </summary>
        public const string ExpirationTime = "exp";

        /// <summary>
        /// JWT生效的开始时间
        /// </summary>
        public const string NotBefore = "nbf";
    }
}
