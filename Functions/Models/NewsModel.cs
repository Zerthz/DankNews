// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Functions.Models
{
    public class Multimedium
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("format")]
        public string? Format { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("subtype")]
        public string? Subtype { get; set; }

        [JsonPropertyName("caption")]
        public string? Caption { get; set; }

        [JsonPropertyName("copyright")]
        public string? Copyright { get; set; }
    }

    public class News
    {
        [JsonPropertyName("section")]
        public string? Section { get; set; }

        [JsonPropertyName("subsection")]
        public string? Subsection { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("abstract")]
        public string? Abstract { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        [JsonPropertyName("byline")]
        public string? Byline { get; set; }

        [JsonPropertyName("item_type")]
        public string? ItemType { get; set; }

        [JsonPropertyName("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [JsonPropertyName("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("published_date")]
        public DateTime PublishedDate { get; set; }

        [JsonPropertyName("material_type_facet")]
        public string? MaterialTypeFacet { get; set; }

        [JsonPropertyName("kicker")]
        public string? Kicker { get; set; }

        [JsonPropertyName("des_facet")]
        public List<string>? DesFacet { get; set; }

        [JsonPropertyName("org_facet")]
        public List<string>? OrgFacet { get; set; }

        [JsonPropertyName("per_facet")]
        public List<string>? PerFacet { get; set; }

        [JsonPropertyName("geo_facet")]
        public List<string>? GeoFacet { get; set; }

        [JsonPropertyName("multimedia")]
        public List<Multimedium>? Multimedia { get; set; }

        [JsonPropertyName("short_url")]
        public string? ShortUrl { get; set; }
    }

    public class NewsModel
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("copyright")]
        public string? Copyright { get; set; }

        [JsonPropertyName("section")]
        public string? Section { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }

        [JsonPropertyName("num_results")]
        public int NumResults { get; set; }

        [JsonPropertyName("results")]
        public List<News> NewsList { get; set; } = new();
    }
}
