using BookRating.App.Models;

namespace BookRating.App.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
    
using Microsoft.Azure.Cosmos;

public class CosmosDbService : ICosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(
        CosmosClient dbClient,
        string databaseName,
        string containerName)
    {
        _container = dbClient.GetContainer(databaseName, containerName);
    }
        
    public async Task AddItemAsync(BookRatingViewModel item)
    {
        await _container.CreateItemAsync(item, new PartitionKey(item.Id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<BookRatingViewModel>(id, new PartitionKey(id));
    }

    public async Task<BookRatingViewModel> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<BookRatingViewModel> response = await _container.ReadItemAsync<BookRatingViewModel>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        { 
            return null;
        }

    }

    public async Task<IEnumerable<BookRatingViewModel>> GetItemsAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<BookRatingViewModel>(new QueryDefinition(queryString));
        List<BookRatingViewModel> results = new List<BookRatingViewModel>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
                
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync(string id, BookRatingViewModel item)
    {
        await _container.UpsertItemAsync(item, new PartitionKey(id));
    }
}