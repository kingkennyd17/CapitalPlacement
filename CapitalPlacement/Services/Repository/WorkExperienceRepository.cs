using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class WorkExperienceRepository : IWorkExperienceRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public WorkExperienceRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<WorkExperience>> CreateAsync(WorkExperience item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.WorkExperienceId));
        }

        public async Task<ItemResponse<WorkExperience>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<WorkExperience>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<WorkExperience>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<WorkExperience>();

            using (FeedIterator<WorkExperience> resultSetIterator = _container.GetItemQueryIterator<WorkExperience>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<WorkExperience> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<WorkExperience>> UpdateAsync(WorkExperience item)
        {
            return await _container.ReplaceItemAsync(item, item.WorkExperienceId, new PartitionKey(item.WorkExperienceId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<WorkExperience>(itemId, new PartitionKey(itemId));
        }

        public async Task<List<WorkExperience>> GetbyApplicationIdAsync(string ApplicationId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.ApplicationId = @ApplicationId")
                .WithParameter("@ApplicationId", ApplicationId);

            var items = new List<WorkExperience>();

            using (FeedIterator<WorkExperience> resultSetIterator = _container.GetItemQueryIterator<WorkExperience>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<WorkExperience> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }
    }
}
