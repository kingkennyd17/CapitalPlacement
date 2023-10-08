using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IProgramDetailsRepository
    {
        Task<ItemResponse<ProgramDetails>> CreateAsync(ProgramDetails model);
        Task<List<ProgramDetails>> GetAllAsync();
        Task<ItemResponse<ProgramDetails>> GetAsync(string Id);
        Task<ItemResponse<ProgramDetails>> UpdateAsync(ProgramDetails model);
        Task DeleteAsync(string Id);
    }
}
