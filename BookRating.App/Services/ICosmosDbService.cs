using BookRating.App.Models;

namespace BookRating.App.Services;

public interface ICosmosDbService
{
    Task<IEnumerable<BookRatingViewModel>> GetItemsAsync(string query);
    Task<BookRatingViewModel> GetItemAsync(string id);
    Task AddItemAsync(BookRatingViewModel item);
    Task UpdateItemAsync(string id, BookRatingViewModel item);
    Task DeleteItemAsync(string id);
}