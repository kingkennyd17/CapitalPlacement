using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class ProgramDetailsRepository : IProgramDetailsRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public ProgramDetailsRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<ProgramDetails>> CreateAsync(ProgramDetails item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.ProgramId));
        }

        public async Task<ItemResponse<ProgramDetails>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<ProgramDetails>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<ProgramDetails>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<ProgramDetails>();

            using (FeedIterator<ProgramDetails> resultSetIterator = _container.GetItemQueryIterator<ProgramDetails>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<ProgramDetails> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<ProgramDetails>> UpdateAsync(ProgramDetails item)
        {
            return await _container.ReplaceItemAsync(item, item.ProgramId, new PartitionKey(item.ProgramId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<ProgramDetails>(itemId, new PartitionKey(itemId));
        }
    }
}
