using System;

namespace DurableFunction.Models
{

    // TODO : Add more meme data?
    public class MemeNewsModel
    {
        public string? NewsTitle { get; set; }
        public string? NewsAbstract { get; set; }
        public string? NewsSection { get; set; }
        public string? NewsSubsection { get; set; }
        public string? NewsURL { get; set; }
        public DateTime NewsDatePublished { get; set; }
        public string? NewsByLine { get; set; }
        public string? MemeURL { get; set; }
        public string? MemeAlt { get; set; }

    }
}