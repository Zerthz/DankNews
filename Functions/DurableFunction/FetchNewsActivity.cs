using System.Threading.Tasks;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using DurableFunction.Models;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;

namespace DurableFunction
{

    public class FetchNewsActivity
    {

        //private readonly static string? token = Environment.GetEnvironmentVariable("nyt-api-token");
        static string token = "dEXY9FNip1rKBlNnrY6L9JYKcr6ZipoW";

        [Function(nameof(FetchNews))]
        public static async Task<System.Collections.Generic.List<DurableFunction.Models.News>?> FetchNews([ActivityTrigger] string foo, FunctionContext context)
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

            foreach (var item in model.NewsList)
            {
                item.PublishedDateString = item.PublishedDate.ToString("dd/MM/yyyy HH:mm");
            }
            return model.NewsList;
        }
    }
}