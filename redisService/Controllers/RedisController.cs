

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using redisService.Dtos;

namespace redisService.Controllers 

{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class RedisApiController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public RedisApiController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet("storedData/{key}")]
        public async Task<IActionResult> Get(string key)
        {
            
            var result = await  _redisService.ReceiveDataAsync(key);
            return Ok(result);
        }

        
        [HttpPost("saveInRedis/{key}")]
        public async Task<IActionResult> Post([FromBody] CreatePromotionDto model, string key)
        {
            await _redisService.SetDataAsync(key, model);
            return Ok();
        }
    }
}
