using Newtonsoft.Json;

namespace BookRating.App.Models;

public class CreateBookRatingViewModel
{
    [JsonProperty(PropertyName = "name")] public string Name { get; set; }

    [JsonProperty(PropertyName = "description")]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "rating")]
    public int Rating { get; set; }
}