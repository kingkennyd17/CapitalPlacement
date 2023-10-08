using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class EducationRepository : IEducationRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public EducationRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<Education>> CreateAsync(Education item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.EducationId));
        }

        public async Task<ItemResponse<Education>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<Education>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<Education>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<Education>();

            using (FeedIterator<Education> resultSetIterator = _container.GetItemQueryIterator<Education>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<Education> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<Education>> UpdateAsync(Education item)
        {
            return await _container.ReplaceItemAsync(item, item.EducationId, new PartitionKey(item.EducationId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<Education>(itemId, new PartitionKey(itemId));
        }

        public async Task<List<Education>> GetbyApplicationIdAsync(string ApplicationId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.ApplicationId = @ApplicationId")
                .WithParameter("@ApplicationId", ApplicationId);

            var items = new List<Education>();

            using (FeedIterator<Education> resultSetIterator = _container.GetItemQueryIterator<Education>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<Education> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }
    }
}
