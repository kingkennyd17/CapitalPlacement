using CapitalPlacement.Models;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Interface
{
    public interface IVideoInterviewRepository
    {
        Task<ItemResponse<VideoInterview>> CreateAsync(VideoInterview model);
        Task<List<VideoInterview>> GetAllAsync();
        Task<ItemResponse<VideoInterview>> GetAsync(string Id);
        Task<ItemResponse<VideoInterview>> UpdateAsync(VideoInterview model);
        Task DeleteAsync(string Id);
    }
}
