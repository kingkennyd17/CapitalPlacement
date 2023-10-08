using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IEducationRepository
    {
        Task<ItemResponse<Education>> CreateAsync(Education model);
        Task<List<Education>> GetAllAsync();
        Task<ItemResponse<Education>> GetAsync(string Id);
        Task<ItemResponse<Education>> UpdateAsync(Education model);
        Task DeleteAsync(string Id);
    }
}
