using CapitalPlacement.DTOs;
using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PreviewController : ControllerBase
    {
        private readonly ApplicationFormRepository _applicationFormService;
        private readonly ProgramDetailsRepository _programDetailsService;

        public PreviewController(IConfiguration configuration)
        {
            _applicationFormService = new ApplicationFormRepository(configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"], configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
            _programDetailsService = new ProgramDetailsRepository(configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"], configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PreviewDTO>> GetApplicationFormDetails(string id)
        {
            PreviewDTO previewDTO = new PreviewDTO();
            previewDTO.backgroundImage = _applicationFormService.GetbyProgramIdAsync(id).Result.CoverImage;
            previewDTO.programDetails = await _programDetailsService.GetAsync(id);

            if (previewDTO == null)
            {
                return NotFound();
            }

            return Ok(previewDTO);
        }
    }
}