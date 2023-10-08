using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IWorkFlowRepository
    {
        Task<ItemResponse<WorkFlow>> CreateAsync(WorkFlow model);
        Task<List<WorkFlow>> GetAllAsync();
        Task<ItemResponse<WorkFlow>> GetAsync(string Id);
        Task<ItemResponse<WorkFlow>> UpdateAsync(WorkFlow model);
        Task DeleteAsync(string Id);
    }
}
