using System;
using System.Text.Json;
using DurableFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace DurableFunction
{

    public static class SaveToQueueActivity
    {
        [Function(nameof(Save))]
        [QueueOutput("newsqueue", Connection = "AzureWebJobsStorage")]
        public static string Save([ActivityTrigger] DurableFunction.Models.MemeNewsModel input)
        {
            var serialiedInput = JsonSerializer.Serialize(input);
            return serialiedInput;
        }
    }
}