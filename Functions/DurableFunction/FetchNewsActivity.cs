using System.Threading.Tasks;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using DurableFunction.Models;
using System.Text.Json;
using System.Collections.Generic;

namespace DurableFunction
{
    [DurableTask(nameof(FetchNewsActivity))]
    public class FetchNewsActivity : TaskActivityBase<string?, List<News>>
    {
        readonly ILogger? logger;

        // TODO : put in a vault or something safe 
        const string token = "dEXY9FNip1rKBlNnrY6L9JYKcr6ZipoW";

        public FetchNewsActivity(ILoggerFactory? loggerFactory)
        {
            logger = loggerFactory?.CreateLogger<FetchNewsActivity>();
        }

        protected override async Task<List<News>?> OnRunAsync(TaskActivityContext context, string? _)
        {
            using HttpClient client = new HttpClient()
            {
                BaseAddress = new Uri($"https://api.nytimes.com/svc/topstories/v2/world.json?api-key={token}")
            };


            var response = await client.GetStringAsync(client.BaseAddress);
            if (response is null)
                throw new NullReferenceException(nameof(response));

            NewsModel model = JsonSerializer.Deserialize<NewsModel>(response)!;
            if (model is null)
                throw new NullReferenceException(nameof(model));


            if (model.NewsList.Count <= 0)
                throw new ArgumentException(nameof(model.NewsList), "doesn't have elements. Which it should have.");

            // Ta bort alla promo grejer som inte är nyheter
            model.NewsList.RemoveAll(n => n.ItemType!.Equals("promo", StringComparison.InvariantCultureIgnoreCase));


            return model.NewsList;
        }
    }
}