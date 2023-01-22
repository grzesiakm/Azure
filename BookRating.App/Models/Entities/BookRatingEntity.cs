using Newtonsoft.Json;

namespace BookRating.App.Models.Entities;

public class BookRatingEntity
{
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }

    [JsonProperty(PropertyName = "name")] public string Name { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "rating")]
    public int Rating { get; set; }

    public static BookRatingEntity FromNewViewModel(CreateBookRatingViewModel createBookRatingViewModel)
    {
        return new BookRatingEntity
        {
            Id = Guid.NewGuid().ToString(),
            Description = createBookRatingViewModel.Description,
            Name = createBookRatingViewModel.Name,
            Rating = createBookRatingViewModel.Rating
        };
    }

    public static BookRatingEntity FromViewModel(BookRatingViewModel bookRatingViewModel)
    {
        return new BookRatingEntity
        {
            Id = bookRatingViewModel.Id,
            Description = bookRatingViewModel.Description,
            Name = bookRatingViewModel.Name,
            Rating = bookRatingViewModel.Rating
        };
    }

    public BookRatingViewModel ToViewModel() => new BookRatingViewModel
    {
        Id = Id,
        Description = Description,
        Name = Name,
        Rating = Rating
    };
}