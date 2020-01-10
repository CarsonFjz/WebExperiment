using Basic.CapWithSugarExtension;
using Basic.Core.ResultModel;
using Basic.SugarExtension;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Collections.Generic;
using System.Threading.Tasks;
using Basic.JwtSecurityTokenExtension;
using Basic.JwtSecurityTokenExtension.Infrastructure;
using WebTest.Model;
using Basic.AuthorizationExtension.RoleAuthorization;

namespace WebTest.Controllers
{
    /// <summary>
    /// default
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// sqlClient
        /// </summary>
        private readonly SqlSugarClient _sugarClient;

        private readonly ICapPublisher _tranPublisher;

        private readonly IJwtSecurityToken _tokenHelper;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sugarClient"></param>
        /// <param name="tokenHelper"></param>
        /// <param name="tranPublisher"></param>
        public ValuesController(SqlSugarClient sugarClient, IJwtSecurityToken tokenHelper, ICapPublisher tranPublisher)
        {
            _sugarClient = sugarClient;
            _tranPublisher = tranPublisher;
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="role"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpGet(nameof(Token))]
        [AllowAnonymous]
        public async Task<TokenOut> Token(string role, string permission)
        {
            var dic = new Dictionary<string, string>
            {
                {"rol", role},//角色
                {"per", permission}//权限
            };

            return await _tokenHelper.CreateToken(dic);
        }

        //[HttpGet(nameof(RefreshToken))]
        //[AllowAnonymous]
        //public ActionResult<TokenOut> RefreshToken(string refreshToken)
        //{
        //    return _tokenHelper.RefreshToken(refreshToken);
        //}


        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[MvcAuthorize]
        //[UnitOfWork] //普通事务
        [CapUnitOfWork] //cap的事务
        public async Task<test> Get()
        {
            var entity = new test() {Name = "cccc"};

            entity.Id = await _sugarClient.Insertable<test>(entity).ExecuteReturnIdentityAsync();

            entity.Name = "asda";

            await _sugarClient.Updateable<test>(entity).ExecuteCommandAsync();

            await _tranPublisher.PublishAsync("test", entity);

            return entity;
        }


        /// <summary>
        /// 错误
        /// </summary>
        /// <returns></returns>
        [HttpGet("Error")]
        [UnitOfWork]
        public async Task<string> Error()
        {
            throw new UserFriendlyException("err");
            //CustomExceptionContext.Throw(new UserFriendlyException(999,"err"));
            return await Task.FromResult("");
        }

        /// <summary>
        /// 错误2
        /// </summary>
        /// <returns></returns>
        [HttpGet("Error2")]
        [RoleCheck("admin")]
        public async Task<string> Error2()
        {
            throw new UserFriendlyException("err");
            //CustomExceptionContext.Throw(new UserFriendlyException(999,"err"));
            return await Task.FromResult("");
        }

        /// <summary>
        /// ModelBinding
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("ModelBinding")]
        [RoleCheck("admin2")]
        public Task<int> ModelBinding([FromBody]TestRequest request)
        {
            return Task.FromResult(1);
        }

        /// <summary>
        /// Content
        /// </summary>
        [HttpPost("Content")]
        public ContentResult Content()
        {
            return Content("111");
        }
    }
}
