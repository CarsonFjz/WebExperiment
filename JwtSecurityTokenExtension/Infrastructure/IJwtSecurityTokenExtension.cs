using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwtSecurityTokenExtension.Infrastructure
{
    public interface IJwtSecurityTokenExtension
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
