using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.DurableTask;
namespace Functions
{
    [DurableTask(nameof(DurableOrchestration))]
    public class DurableOrchestration : TaskOrchestratorBase<string, List<string>>
    {

        protected async override Task<List<string>?> OnRunAsync(TaskOrchestrationContext context, string? input)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallSayHelloActivityAsync("Tokyo"));
            outputs.Add(await context.CallSayHelloActivityAsync("Seattle"));
            outputs.Add(await context.CallSayHelloActivityAsync("London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            foreach (var item in outputs)
            {
                System.Console.WriteLine(item);
            }

            var newsModel = await context.CallFetchNewsActivityAsync("_");
            return outputs;
        }
    }

    public static class OrchestrationStarter
    {

        [Function(nameof(HttpStart))]
        public static async Task<HttpResponseData> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
           [DurableClient] DurableClientContext starter,
           FunctionContext executionContext)
        {

            ILogger logger = executionContext.GetLogger(nameof(HttpStart));
            // Function input comes from the request content.
            string instanceId = await starter.Client.ScheduleNewDurableOrchestrationInstanceAsync();

            logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}