using BookRating.App.Models;
using BookRating.App.Models.Entities;

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

    public async Task AddItemAsync(BookRatingEntity entity)
    {
        await _container.CreateItemAsync(entity, new PartitionKey(entity.Id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<BookRatingViewModel>(id, new PartitionKey(id));
    }

    public async Task<BookRatingEntity> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<BookRatingEntity> response =
                await _container.ReadItemAsync<BookRatingEntity>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<BookRatingEntity>> GetItemsAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<BookRatingEntity>(new QueryDefinition(queryString));
        List<BookRatingEntity> results = new List<BookRatingEntity>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync(BookRatingEntity entity)
    {
        await _container.UpsertItemAsync(entity, new PartitionKey(entity.Id));
    }
}