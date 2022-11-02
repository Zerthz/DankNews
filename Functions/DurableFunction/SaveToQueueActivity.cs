using System;
using DurableFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace DurableFunction
{

    [DurableTask(nameof(SaveToQueueActivity))]
    public class SaveToQueueActivity : TaskActivityBase<string, string>
    {
        [QueueOutput("newsqueue", Connection = "AzureWebJobsStorage")]
        protected override string? OnRun(TaskActivityContext context, string? input)
        {

            return input;
        }

    }

    // public static class SaveToQueueActivity
    // {
    //     [Function("SaveToQueue")]
    //     [QueueOutput("newsqueue", Connection = "AzureWebJobsStorage")]
    //     public static string[] Save([ActivityTrigger] string input)
    //     {
    //         string[] messages = {
    //             $"Message = {input}",
    //             $"Id = {Guid.NewGuid()}"
    //         };
    //         return messages;
    //     }
    // }
}