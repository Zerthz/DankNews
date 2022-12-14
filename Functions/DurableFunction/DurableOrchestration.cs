using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.DurableTask;
using DurableFunction.Models;

namespace DurableFunction
{
    // Typed är egentligen nyare men kan inte få det att funka med queue output för att rädda mitt liv..  
    public class DurableOrchestration
    {
        [Function(nameof(RunOrchestration))]
        public static async Task<string> RunOrchestration([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            // News
            var newsList = await context.CallActivityAsync<List<News>>(nameof(FetchNewsActivity.FetchNews), "");
            // Memes
            var memesList = await context.CallActivityAsync<List<Meme>>(nameof(GetMemes.FetchMemes), "");
            // Assembled
            var assembled = await context.CallActivityAsync<List<MemeNewsModel>>(nameof(AssembleDataActivity.AssembleMemeNews), new AssembleInput(newsList, memesList));
            // Save
            // memenews to test
            foreach (var item in assembled)
            {
                await context.CallActivityAsync<string>(nameof(SaveToQueueActivity.Save), item);
            }

            return null!;
        }

        [Function(nameof(HttpStart))]
        public static async Task<string> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
           [DurableClient] DurableClientContext starter,
           FunctionContext executionContext)
        {

            ILogger logger = executionContext.GetLogger(nameof(HttpStart));
            // Function input comes from the request content.
            string instanceId = await starter.Client.ScheduleNewOrchestrationInstanceAsync(nameof(RunOrchestration));

            logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");


            return instanceId;
        }
    }


}