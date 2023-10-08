using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IApplicationFormRepository
    {
        Task<ItemResponse<ApplicationForm>> CreateAsync(ApplicationForm model);
        Task<List<ApplicationForm>> GetAllAsync();
        Task<ItemResponse<ApplicationForm>> GetAsync(string Id);
        Task<ItemResponse<ApplicationForm>> UpdateAsync(ApplicationForm model);
        Task DeleteAsync(string Id);
    }
}
