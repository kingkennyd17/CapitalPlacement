using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProgramDetailsController : ControllerBase
    {
        private readonly ProgramDetailsRepository _programDetailsService;

        public ProgramDetailsController(IConfiguration configuration)
        {
            _programDetailsService = new ProgramDetailsRepository(
                configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"],
                configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<ProgramDetails>> CreateAsync(ProgramDetails item)
        {
            var response = await _programDetailsService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramDetails>> GetAsync(string id)
        {
            var response = await _programDetailsService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProgramDetails>>> GetAllAsync()
        {
            var response = await _programDetailsService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ProgramDetails>> UpdateAsync(ProgramDetails item)
        {
            var response = await _programDetailsService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _programDetailsService.DeleteAsync(id);
            return NoContent();
        }
    }
}