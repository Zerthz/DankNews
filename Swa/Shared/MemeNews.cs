using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class MemeNews
    {
        public Guid? id { get; set; }
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
