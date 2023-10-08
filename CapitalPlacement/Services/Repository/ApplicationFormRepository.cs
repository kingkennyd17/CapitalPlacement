using CapitalPlacement.Models;
using CapitalPlacement.Services.Interface;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacement.Services.Repository
{
    public class ApplicationFormRepository : IApplicationFormRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public ApplicationFormRepository(string endpointUri, string primaryKey, string databaseId, string containerId)
        {
            _cosmosClient = new CosmosClient(endpointUri, primaryKey);
            var database = _cosmosClient.GetDatabase(databaseId);
            _container = database.GetContainer(containerId);
        }

        public async Task<ItemResponse<ApplicationForm>> CreateAsync(ApplicationForm item)
        {
            return await _container.CreateItemAsync(item, new PartitionKey(item.ApplicationId));
        }

        public async Task<ItemResponse<ApplicationForm>> GetAsync(string itemId)
        {
            try
            {
                return await _container.ReadItemAsync<ApplicationForm>(itemId, new PartitionKey(itemId));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<List<ApplicationForm>> GetAllAsync()
        {
            var query = new QueryDefinition("SELECT * FROM c");
            var items = new List<ApplicationForm>();

            using (FeedIterator<ApplicationForm> resultSetIterator = _container.GetItemQueryIterator<ApplicationForm>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<ApplicationForm> response = await resultSetIterator.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return items;
        }

        public async Task<ItemResponse<ApplicationForm>> UpdateAsync(ApplicationForm item)
        {
            return await _container.ReplaceItemAsync(item, item.ApplicationId, new PartitionKey(item.ApplicationId));
        }

        public async Task DeleteAsync(string itemId)
        {
            await _container.DeleteItemAsync<ApplicationForm>(itemId, new PartitionKey(itemId));
        }

        public async Task<ApplicationForm> GetbyProgramIdAsync(string ProgramId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.ProgramId = @ProgramId")
                .WithParameter("@ProgramId", ProgramId);

            using (FeedIterator<ApplicationForm> resultSetIterator = _container.GetItemQueryIterator<ApplicationForm>(query))
            {
                if (resultSetIterator.HasMoreResults)
                {
                    FeedResponse<ApplicationForm> response = await resultSetIterator.ReadNextAsync();
                    return response.FirstOrDefault();
                }
                return null;
            }
        }
    }
}
