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
            try
            {
                var json = JsonSerializer.Serialize(jsonData);
                var id = await _mongoService.InsertJsonAsync(json);

                return Ok(new { id });
            }
            catch (Exception ex) { 
                return BadRequest(new { errorMsg = ex.Message });
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetJson(string id)
        {
            var document = await _mongoService.GetJsonAsync(id);

            if (document == null) { 
                return NotFound(new {errorMsg = "Id not found!"});
            }

            return Ok(document);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllJson()
        {
            var json = await _mongoService.getAll();
            Console.WriteLine(json);
            return Ok(json);
        }
    }
}
