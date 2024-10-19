

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace REDISSERVICE.Controllers 

{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("storedData/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            
            var result = await  _redisService.ReceiveDataAsync(key);
            return Ok(result);
        }

        public class MyDataModel
        {
            public string Property1 { get; set; } ="";
            public int Property2 { get; set; } =0;
        }
        [HttpPost("saveInRedis")]
        public async Task<IActionResult> Post([FromBody] MyDataModel model)
        {
            await _redisService.SetDataAsync("test", model);
            return Ok();
        }
    }
}
