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
                SqlQuery = "SELECT top 1 * FROM MemeNews m order by m.NewsDatePublished desc",
                ConnectionStringSetting = "MemeNewsDb")] IEnumerable<MemeNews> memeNews,
            ILogger log)
        {
            if (memeNews is null)
            {
                return new NotFoundResult();
            }

            foreach (var meme in memeNews)
            {
                log.LogInformation(meme.NewsTitle);
            }

            return new OkObjectResult(memeNews.ToList());
        }
    }
}
