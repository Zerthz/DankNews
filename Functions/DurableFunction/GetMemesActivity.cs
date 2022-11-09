using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using DurableFunction.Models;
using System;
using System.Collections.Generic;

namespace DurableFunction
{
    public static class GetMemes
    {

        private static HttpClient httpClient = new HttpClient();



        [Function(nameof(FetchMemes)]
        public static async Task<System.Collections.Generic.List<DurableFunction.Models.Meme>> FetchMemes([ActivityTrigger] string discard, FunctionContext context)
        {

            var getPictureResponse = await httpClient.GetAsync("https://api.imgflip.com/get_memes");

            getPictureResponse.EnsureSuccessStatusCode();
            var content = await getPictureResponse.Content.ReadAsStringAsync();

            if (content is null)
                throw new ArgumentNullException(nameof(content) + " is null");

            Root model = JsonSerializer.Deserialize<Root>(content)!;
            return model.Data!.Memes!;
        }
    }
}
