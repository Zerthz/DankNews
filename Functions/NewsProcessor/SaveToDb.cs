using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using NewsProcessor.Cosmos;

namespace StorageFunction
{
    public class SaveToDb
    {
        private readonly ILogger _logger;
        private CosmosDbConn _dbConn;

        public SaveToDb(ILoggerFactory loggerFactory, CosmosDbConn dbConn)
        {
            _logger = loggerFactory.CreateLogger<SaveToDb>();
            _dbConn = dbConn;
        }

        [Function("SaveToCosmos")]
        public async Task Run([QueueTrigger("newsqueue", Connection = "AzureWebJobsStorage")] string newsItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {newsItem}");
            await _dbConn.SaveToCosmos(newsItem);
        }
    }
}
