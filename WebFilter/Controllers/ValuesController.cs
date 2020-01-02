using Basic.Core.Exceptions;
using Basic.SugarExtension;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using WebTest.Model;

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
        public readonly SqlSugarClient _sugarClient;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sugarClient"></param>
        public ValuesController(SqlSugarClient sugarClient)
        {
            _sugarClient = sugarClient;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[MvcAuthorize]
        [UnitOfWork]
        public async Task<Test> Get()
        {
            var entity = new Test() {Name = "cccc"};

            entity.Id = await _sugarClient.Insertable<Test>(entity).ExecuteReturnIdentityAsync();

            entity.Name = "asda";

            throw new UserFriendlyException(300,"m");

            await _sugarClient.Updateable<Test>(entity).ExecuteCommandAsync();

            return entity;
        }

        /// <summary>
        /// ModelBinding
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("ModelBinding")]
        public Task<int> ModelBinding([FromBody]TestRequest request)
        {
            return Task.FromResult(1);
        }
    }
}
