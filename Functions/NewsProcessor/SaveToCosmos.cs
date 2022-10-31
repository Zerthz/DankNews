using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace StorageFunction
{
    public class SaveToCosmos
    {
        private readonly ILogger _logger;

        public SaveToCosmos(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SaveToCosmos>();
        }

        [Function("SaveToCosmos")]
        public void Run([QueueTrigger("myqueue-items", Connection = "stteknikaaf_STORAGE")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
