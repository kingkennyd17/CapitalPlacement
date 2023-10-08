using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class VideoInterviewController : ControllerBase
    {
        private readonly VideoInterviewRepository _videoInterviewService;

        public VideoInterviewController(IConfiguration configuration)
        {
            _videoInterviewService = new VideoInterviewRepository(
                configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"],
                configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<VideoInterview>> CreateAsync(VideoInterview item)
        {
            var response = await _videoInterviewService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VideoInterview>> GetAsync(string id)
        {
            var response = await _videoInterviewService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoInterview>>> GetAllAsync()
        {
            var response = await _videoInterviewService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<VideoInterview>> UpdateAsync(VideoInterview item)
        {
            var response = await _videoInterviewService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _videoInterviewService.DeleteAsync(id);
            return NoContent();
        }
    }
}