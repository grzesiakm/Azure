using BookRating.App.Models;

namespace BookRating.App.Services
{
	public interface IBookRatingCosmosService
    {
        object BookRatingViewModel { get; set; }

        Task<List<BookRatingViewModel>> Get(string sqlCosmosQuery);
        Task<BookRatingViewModel> Create(BookRatingViewModel newMovie);
        Task<BookRatingViewModel> Edit(BookRatingViewModel movieToUpdate);
        Task Delete(string id, string type);
    }

}
