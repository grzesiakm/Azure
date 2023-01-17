using BookRating.App.Models;

namespace BookRating.App.Services;

public class MockCosmosDbService : ICosmosDbService
{
    private readonly BookRatingViewModel _mock = new BookRatingViewModel
    {
        Id = "id",
        Description = "abcd",
        Completed = false,
        Name = "film o pszczołach"
    };
    public Task<IEnumerable<BookRatingViewModel>> GetItemsAsync(string query)
    {
        return Task.FromResult(new []
        {
            _mock
            
        }.AsEnumerable());
    }

    public Task<BookRatingViewModel> GetItemAsync(string id)
    {
        return Task.FromResult(_mock);
    }

    public  Task AddItemAsync(BookRatingViewModel item) => Task.CompletedTask;

    public async Task UpdateItemAsync(string id, BookRatingViewModel item)
    {
        
    }

    public async Task DeleteItemAsync(string id)
    {
        
    }
}