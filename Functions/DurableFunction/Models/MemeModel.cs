using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DurableFunction.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class MemeData
    {
        [JsonPropertyName("memes")]
        public List<Meme>? Memes { get; set; }
    }

    public class Meme
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("box_count")]
        public int BoxCount { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public MemeData? Data { get; set; }
    }


}