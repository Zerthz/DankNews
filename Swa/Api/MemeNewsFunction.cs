using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using BlazorApp.Shared;

namespace BlazorApp.Api
{
    public static class MemeNewsFunction
    {

        [FunctionName("GetMemeNews")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
                Route = null)]HttpRequest req,
            [CosmosDB(
                databaseName: "MemeNewsDb",
                collectionName: "MemeNews",
                ConnectionStringSetting = "https://cosmos-teknik-aaf.documents.azure.com:443/")] CosmosClient client,
            ILogger log)
        {

            Microsoft.Azure.Cosmos.Container container = client.GetDatabase("MemeNewsDb").GetContainer("MemeNews");

            QueryDefinition queryDefinition = new QueryDefinition(
                "SELECT top 100 * FROM MemeNews order by MemeNews.NewsDatePublished desc");

            using (FeedIterator<MemeNews> resultSet = container.GetItemQueryIterator<MemeNews>(queryDefinition))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<MemeNews> response = await resultSet.ReadNextAsync();
                    MemeNews item = response.First();
                    log.LogInformation(item.NewsByLine);
                }
            }

            return new OkResult();
        }
    }
}
