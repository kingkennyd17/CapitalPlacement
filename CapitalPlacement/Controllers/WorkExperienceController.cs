using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkExperienceController : ControllerBase
    {
        private readonly WorkExperienceRepository _workExperienceService;

        public WorkExperienceController(IConfiguration configuration)
        {
            _workExperienceService = new WorkExperienceRepository(
                configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"],
                configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<WorkExperience>> CreateAsync(WorkExperience item)
        {
            var response = await _workExperienceService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkExperience>> GetAsync(string id)
        {
            var response = await _workExperienceService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkExperience>>> GetAllAsync()
        {
            var response = await _workExperienceService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<WorkExperience>> UpdateAsync(WorkExperience item)
        {
            var response = await _workExperienceService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _workExperienceService.DeleteAsync(id);
            return NoContent();
        }
    }
}