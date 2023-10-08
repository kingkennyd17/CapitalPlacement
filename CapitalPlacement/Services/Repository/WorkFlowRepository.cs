using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class WorkFlowRepository : IWorkFlowRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public WorkFlowRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<WorkFlow>> CreateAsync(WorkFlow item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.WorkFlowId));
        }

        public async Task<ItemResponse<WorkFlow>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<WorkFlow>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<WorkFlow>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<WorkFlow>();

            using (FeedIterator<WorkFlow> resultSetIterator = _container.GetItemQueryIterator<WorkFlow>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<WorkFlow> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<WorkFlow>> UpdateAsync(WorkFlow item)
        {
            return await _container.ReplaceItemAsync(item, item.WorkFlowId, new PartitionKey(item.WorkFlowId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<WorkFlow>(itemId, new PartitionKey(itemId));
        }
    }
}
