using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.DurableTask;
namespace Functions
{
    // Typed är mobbat när vi inte behöver ngt för input, men det är den nyare teknologin enligt docs på github
    [DurableTask(nameof(DurableOrchestration))]
    public class DurableOrchestration : TaskOrchestratorBase<string, List<string>>
    {

        protected async override Task<List<string>?> OnRunAsync(TaskOrchestrationContext context, string? _)
        {

            var newsList = await context.CallFetchNewsActivityAsync("_");


            return null;
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