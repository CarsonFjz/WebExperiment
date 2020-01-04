using Basic.CapWithSugarExtension;
using Basic.SugarExtension;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Threading.Tasks;
using DotNetCore.CAP;
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
        private readonly SqlSugarClient _sugarClient;

        private readonly ICapPublisher _tranPublisher;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sugarClient"></param>
        /// <param name="tranPublisher"></param>
        public ValuesController(SqlSugarClient sugarClient, ICapPublisher tranPublisher)
        {
            _sugarClient = sugarClient;
            _tranPublisher = tranPublisher;
        }
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
        /// ModelBinding
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("ModelBinding")]
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
