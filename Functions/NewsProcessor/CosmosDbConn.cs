using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NewsProcessor.Models;
using System.Configuration;

namespace NewsProcessor.Cosmos
{
    public class CosmosDbConn
    {
        readonly ILogger logger;
        const string endpoint = "https://cosmos-teknik-aaf.documents.azure.com:443/";
        private readonly string? key = Environment.GetEnvironmentVariable("cosmos-dev-key");
        const string dbId = "MemeNewsDb";
        const string containerID = "MemeNews";



        public CosmosDbConn(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<CosmosDbConn>();
        }

        public async Task SaveToCosmos(MemeNewsDTO itemToSave)
        {
            try
            {
                if (key is null)
                    throw new NullReferenceException(nameof(key) + " is null, can't create connection");

                using CosmosClient client = new CosmosClient(endpoint, key);
                var container = client.GetContainer(dbId, containerID);

                logger.LogInformation($"Saving {itemToSave} to database");

                itemToSave.id = Guid.NewGuid();
                await container.CreateItemAsync<MemeNewsDTO>(itemToSave);

                logger.LogInformation("Saved successfully");
            }
            catch (CosmosException cre)
            {
                logger.LogError(cre.ToString());
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                logger.LogError("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
        }

    }
}