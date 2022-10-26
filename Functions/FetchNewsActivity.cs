using System.Threading.Tasks;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using Functions.Models;
using System.Text.Json;

namespace Functions
{
    [DurableTask(nameof(FetchNewsActivity))]
    public class FetchNewsActivity : TaskActivityBase<string, NewsModel>
    {
        readonly ILogger? logger;

        // TODO : put in a vault or something safe 
        const string token = "dEXY9FNip1rKBlNnrY6L9JYKcr6ZipoW";

        public FetchNewsActivity(ILoggerFactory? loggerFactory)
        {
            logger = loggerFactory?.CreateLogger<FetchNewsActivity>();
        }

        protected override async Task<NewsModel?> OnRunAsync(TaskActivityContext context, string? input)
        {
            using HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri($"https://api.nytimes.com/svc/topstories/v2/world.json?api-key={token}")
            };


            var response = await client.GetStringAsync(client.BaseAddress);
            if (response is null)
                throw new ArgumentNullException(nameof(response));

            NewsModel model = JsonSerializer.Deserialize<NewsModel>(response)!;

            return model;
        }
    }
}