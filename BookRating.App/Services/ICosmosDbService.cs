using BookRating.App.Models.Entities;

namespace BookRating.App.Services;

public interface ICosmosDbService
{
    Task<IEnumerable<BookRatingEntity>> GetItemsAsync(string query);
    Task<BookRatingEntity> GetItemAsync(string id);
    Task AddItemAsync(BookRatingEntity entity);
    Task UpdateItemAsync(BookRatingEntity entity);
    Task DeleteItemAsync(string id);
}