using CapitalPlacement.DTOs;
using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using CapitalPlacement.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkFlowController : ControllerBase
    {
        private readonly WorkFlowRepository _workFlowService;
        private readonly VideoInterviewRepository _videoInterviewService;

        public WorkFlowController(IConfiguration configuration)
        {
            _workFlowService = new WorkFlowRepository(configuration["CosmosDB:EndpointUri"], configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
            _videoInterviewService = new VideoInterviewRepository(configuration["CosmosDB:EndpointUri"], configuration["CosmosDB:PrimaryKey"],
                configuration["CosmosDB:DatabaseId"], configuration["CosmosDB:ContainerId"]
            );
        }

        [HttpPost]
        public async Task<ActionResult<WorkFlow>> CreateAsync(WorkFlow item)
        {
            var response = await _workFlowService.CreateAsync(item);
            return Ok(response.Resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkFlow>> GetAsync(string id)
        {
            var response = await _workFlowService.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response.Resource);
        }

        [HttpGet]
        public async Task<ActionResult<List<WorkFlow>>> GetAllAsync()
        {
            var response = await _workFlowService.GetAllAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<WorkFlow>> UpdateAsync(WorkFlow item)
        {
            var response = await _workFlowService.UpdateAsync(item);
            return Ok(response.Resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _workFlowService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkFlowDTO>> GetApplicationFormDetails(string id)
        {
            WorkFlowDTO workflowDTO = new WorkFlowDTO();
            workflowDTO.workFlow = await _workFlowService.GetAsync(id);
            workflowDTO.videoInterview = await _videoInterviewService.GetbyWorkFlowIdAsync(id);

            if (workflowDTO == null)
            {
                return NotFound();
            }

            return Ok(workflowDTO);
        }
    }
}