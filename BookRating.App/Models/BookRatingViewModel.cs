using System;
using Newtonsoft.Json;

namespace BookRating.App.Models
{
	public class BookRatingViewModel
	{
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public int Rating { get; set; }
    }
}

