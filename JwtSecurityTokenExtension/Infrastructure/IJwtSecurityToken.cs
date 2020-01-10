using System.Collections.Generic;
using System.Threading.Tasks;

namespace Basic.JwtSecurityTokenExtension.Infrastructure
{
    public interface IJwtSecurityToken
    {
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="claimsContent"></param>
        /// <returns></returns>
        Task<TokenOut> CreateToken(Dictionary<string, string> claimsContent = null);

        /// <summary>
        /// 创建token 带refresh
        /// </summary>
        /// <param name="claimsContent"></param>
        /// <returns></returns>
        Task<TokenWithRefreshTokenOut> CreateTokenWithRefresh(Dictionary<string, string> claimsContent = null);
    }
}
