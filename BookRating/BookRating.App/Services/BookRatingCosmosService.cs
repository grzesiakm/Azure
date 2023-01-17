using BookRating.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace BookRating.App.Services
{
	public class BookRatingCosmosService : IBookRatingCosmosService
	{
        private readonly Container _container;
        public BookRatingCosmosService(CosmosClient cosmosClient,
        string databaseName,
        string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<List<BookRatingViewModel>> Get(string sqlCosmosQuery)
        {
            var query = _container.GetItemQueryIterator<BookRatingViewModel>(new QueryDefinition(sqlCosmosQuery));

            List<BookRatingViewModel> result = new List<BookRatingViewModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }

        public async Task<BookRatingViewModel> Create(BookRatingViewModel newBookRating)
        {
            var item = await _container.CreateItemAsync<BookRatingViewModel>(newBookRating, new PartitionKey(newBookRating.Type));
            return item;
        }

        public async Task<BookRatingViewModel> Edit(BookRatingViewModel bookRatingToEdit)
        {
            var item = await _container.UpsertItemAsync<BookRatingViewModel>(bookRatingToEdit, new PartitionKey(bookRatingToEdit.Type));
            return item;
        }

        public async Task Delete(string id, string type)
        {
            await _container.DeleteItemAsync<BookRatingViewModel>(id, new PartitionKey(type));
        }
    }
}

