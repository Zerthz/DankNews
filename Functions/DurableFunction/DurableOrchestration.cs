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
    // Typed är mobbat när vi inte behöver ngt för input, men det är den nyare teknologin enligt docs på github
    [DurableTask(nameof(DurableOrchestration))]
    public class DurableOrchestration : TaskOrchestratorBase<string, string>
    {
        protected async override Task<string?> OnRunAsync(TaskOrchestrationContext context, string? _)
        {

            var newsList = await context.CallFetchNewsActivityAsync("_");

            // memenews to test
            MemeNewsModel model = new MemeNewsModel()
            {
                NewsTitle = "HelloWorld",
                NewsAbstract = "",
                NewsSection = "",
                NewsSubsection = "",
                NewsURL = "",
                NewsDatePublished = System.DateTime.Now,
                NewsByLine = "",
                MemeURL = ""
            };


            var foo = await context.CallSaveToQueueActivityAsync("Hello World");
            return "Hello World";
        }
    }

    public static class OrchestrationStarter
    {
        // Det här är det enda sättet jag har lyckats att outputta till en queue. försökt mycket inget funkat förutom
        [Function(nameof(HttpStart))]
        [QueueOutput("newsqueue", Connection = "AzureWebJobsStorage")]
        public static async Task<string> HttpStart(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
           [DurableClient] DurableClientContext starter,
           FunctionContext executionContext)
        {

            ILogger logger = executionContext.GetLogger(nameof(HttpStart));
            // Function input comes from the request content.
            string instanceId = await starter.Client.ScheduleNewDurableOrchestrationInstanceAsync();

            logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");


            return instanceId;
        }
    }
}