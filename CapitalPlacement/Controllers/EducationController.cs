using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EducationController : ControllerBase
    {
        private readonly EducationRepository _educationService;

        public EducationController(IConfiguration configuration)
        {
            _educationService = new EducationRepository(
                configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"],
                configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<Education>> CreateAsync(Education item)
        {
            var response = await _educationService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Education>> GetAsync(string id)
        {
            var response = await _educationService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<Education>>> GetAllAsync()
        {
            var response = await _educationService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Education>> UpdateAsync(Education item)
        {
            var response = await _educationService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _educationService.DeleteAsync(id);
            return NoContent();
        }
    }
}