using Microsoft.AspNetCore.Mvc;
using simpleAPI.Services;
using System.Text.Json;

namespace simpleAPI.Controllers
{
    [ApiController]
    [Route("api/mongo")]
    public class MongoController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public MongoController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveJson([FromBody] object jsonData)
        {
            var json = JsonSerializer.Serialize(jsonData);
            var id = await _mongoService.InsertJsonAsync(json);

            return Ok(new {id});
        }
    }
}
