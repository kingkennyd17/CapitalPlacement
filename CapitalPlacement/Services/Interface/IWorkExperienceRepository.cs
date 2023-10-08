using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IWorkExperienceRepository
    {
        Task<ItemResponse<WorkExperience>> CreateAsync(WorkExperience model);
        Task<List<WorkExperience>> GetAllAsync();
        Task<ItemResponse<WorkExperience>> GetAsync(string Id);
        Task<ItemResponse<WorkExperience>> UpdateAsync(WorkExperience model);
        Task DeleteAsync(string Id);
    }
}
