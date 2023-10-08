using CapitalPlacement.DTOs;
using CapitalPlacement.Models;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ApplicationFormController : ControllerBase
    {
        private readonly ApplicationFormRepository _applicationFormService;
        private readonly EducationRepository _educationService;
        private readonly WorkExperienceRepository _workExperienceService;

        public ApplicationFormController(IConfiguration configuration)
        {
            _applicationFormService = new ApplicationFormRepository(configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"], configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
            _educationService = new EducationRepository(configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"], configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
            _workExperienceService = new WorkExperienceRepository(configuration["CosmosDB:EndpointUri"],
                configuration["CosmosDB:PrimaryKey"], configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationForm>> CreateAsync(ApplicationForm item)
        {
            var response = await _applicationFormService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationForm>> GetAsync(string id)
        {
            var response = await _applicationFormService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationForm>>> GetAllAsync()
        {
            var response = await _applicationFormService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApplicationForm>> UpdateAsync(ApplicationForm item)
        {
            var response = await _applicationFormService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _applicationFormService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationFormDTO>> GetApplicationFormDetails(string id)
        {
            ApplicationFormDTO applicationFormDTO = new ApplicationFormDTO();
            applicationFormDTO.applicationForm = await _applicationFormService.GetAsync(id);
            applicationFormDTO.education = await _educationService.GetbyApplicationIdAsync(id);
            applicationFormDTO.workExperience = await _workExperienceService.GetbyApplicationIdAsync(id);

            if (applicationFormDTO == null)
            {
                return NotFound();
            }

            return Ok(applicationFormDTO);
        }
    }
}