using System.Text.Json.Serialization;

namespace NewsProcessor.Models
{
    public class MemeNewsDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public string? NewsTitle { get; set; }
        public string? NewsAbstract { get; set; }
        public string? NewsSection { get; set; }
        public string? NewsSubsection { get; set; }
        public string? NewsURL { get; set; }
        public DateTime NewsDatePublished { get; set; }
        public string? NewsByLine { get; set; }
        public string? MemeURL { get; set; }
    }
}