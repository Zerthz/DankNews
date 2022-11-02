using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using NewsProcessor.Models;

namespace NewsProcessor.Cosmos
{
    public class CosmosDbConn
    {
        readonly ILogger logger;

        // TODO : iaf key borde ligga i en keyvault
        const string endpoint = "https://cosmos-teknik-aaf.documents.azure.com:443/";
        const string key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

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