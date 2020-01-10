using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Basic.JwtSecurityTokenExtension.Infrastructure;

namespace Basic.JwtSecurityTokenExtension.Implementation
{
    public class JwtSecurityToken : JwtSecurityTokenHandler, IJwtSecurityToken
    {
        protected readonly JwtOption _jwtOption;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly DateTime _currentTime = DateTime.Now;
        
        public JwtSecurityToken(JwtOption jwtOption, IRefreshTokenStore refreshTokenStore)
        {
            _jwtOption = jwtOption;
            _refreshTokenStore = refreshTokenStore;
        }

        public async Task<TokenOut> CreateToken(Dictionary<string, string> claimsContent = null)
        {
            return await Task.FromResult(new TokenOut()
            {
                Token = CreateEncodedJwt(claimsContent),
                TokenExpires = _currentTime.AddMinutes(_jwtOption.AccessTokenExpiresMinutes),
            });
        }

        /// <summary>
        /// 创建token和refreshToken
        /// </summary>
        /// <param name="claimsContent"></param>
        /// <returns></returns>
        public async Task<TokenWithRefreshTokenOut> CreateTokenWithRefresh(Dictionary<string, string> claimsContent = null)
        {
            return await Task.FromResult(new TokenWithRefreshTokenOut()
            {
                Token = CreateEncodedJwt(claimsContent),
                TokenExpires = _currentTime.AddMinutes(_jwtOption.AccessTokenExpiresMinutes),
                RefreshToken = CreateRefreshCode(claimsContent, out DateTime refreshTokenExpires),
                RefreshTokenExpires = refreshTokenExpires
            });
        }

        /// <summary>
        /// 通过refreshToken获取token信息
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public TokenWithRefreshTokenOut RefreshToken(string refreshToken)
        {
            var refreshModel = _refreshTokenStore.Get(refreshToken);

            if (refreshModel != null)
            {
                return new TokenWithRefreshTokenOut()
                {
                    Token = CreateEncodedJwt(refreshModel.ClaimsContent),
                    TokenExpires = _currentTime.AddMinutes(_jwtOption.AccessTokenExpiresMinutes),
                    RefreshToken = refreshToken,
                    RefreshTokenExpires = refreshModel.ExpiresAt
                };
            }

            return null;
        }

        private string CreateEncodedJwt(Dictionary<string, string> claimsContent = null)
        {
            var jwtDesc = CreateSecurityTokenDescriptor(claimsContent);

            return CreateEncodedJwt(jwtDesc);
        }

        protected SecurityTokenDescriptor CreateSecurityTokenDescriptor(Dictionary<string, string> claimsContent)
        {
            var expiresAt = _currentTime.AddMinutes(_jwtOption.AccessTokenExpiresMinutes);//到期时间

            var claimsIdentity = new ClaimsIdentity();

            if (claimsContent != null)
            {
                foreach (var item in claimsContent)
                {
                    if (item.Value != null)
                    {
                        claimsIdentity.AddClaim(new Claim(item.Key, item.Value));
                    }
                }
            }

            var jwtDesc = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,//jwt主体
                Expires = expiresAt,//过期时间
                NotBefore = _currentTime,//jwt开始时间
                IssuedAt = _currentTime,//jwt签发时间
                SigningCredentials = 
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOption.IssuerSigningKey)), 
                        "HS256")
            };

            if (!string.IsNullOrEmpty(_jwtOption.Issuer))
            {
                jwtDesc.Issuer = _jwtOption.Issuer;
            }

            if (!string.IsNullOrEmpty(_jwtOption.Audience))
            {
                jwtDesc.Audience = _jwtOption.Audience;
            }

            return jwtDesc;
        }


        /// <summary>
        /// 创建refreshCode
        /// </summary>
        /// <param name="claimsContent"></param>
        /// <param name="refreshTokenExpires"></param>
        /// <returns></returns>
        private string CreateRefreshCode(Dictionary<string, string> claimsContent, out DateTime refreshTokenExpires)
        {
            var guid = Guid.NewGuid().ToString();

            byte[] b = System.Text.Encoding.Default.GetBytes(guid);

            var refreshToken = Convert.ToBase64String(b);

            var expiresAt = _currentTime.AddMinutes(_jwtOption.RefreshTokenExpiresMinutes);

            var refreshTokenModel = new RefreshTokenModel()
            {
                ClaimsContent = claimsContent,
                ExpiresAt = expiresAt
            };

            var timeSpan = new TimeSpan(expiresAt.Ticks);

            _refreshTokenStore.Set(refreshToken, refreshTokenModel, timeSpan);

            refreshTokenExpires = refreshTokenModel.ExpiresAt;

            return refreshToken;
        }
    }
}
