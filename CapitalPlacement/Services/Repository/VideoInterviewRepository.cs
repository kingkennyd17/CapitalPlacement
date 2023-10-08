using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class VideoInterviewRepository : IVideoInterviewRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public VideoInterviewRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<VideoInterview>> CreateAsync(VideoInterview item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.VideoInterviewId));
        }

        public async Task<ItemResponse<VideoInterview>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<VideoInterview>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<VideoInterview>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<VideoInterview>();

            using (FeedIterator<VideoInterview> resultSetIterator = _container.GetItemQueryIterator<VideoInterview>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<VideoInterview> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<VideoInterview>> UpdateAsync(VideoInterview item)
        {
            return await _container.ReplaceItemAsync(item, item.VideoInterviewId, new PartitionKey(item.VideoInterviewId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<VideoInterview>(itemId, new PartitionKey(itemId));
        }

        public async Task<List<VideoInterview>> GetbyWorkFlowIdAsync(string WorkFlowId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.WorkFlowId = @WorkFlowId")
                .WithParameter("@WorkFlowId", WorkFlowId);

            var items = new List<VideoInterview>();

            using (FeedIterator<VideoInterview> resultSetIterator = _container.GetItemQueryIterator<VideoInterview>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<VideoInterview> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }
    }
}
