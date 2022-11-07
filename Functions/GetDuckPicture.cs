using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company.Function
{
    public class GetDuckPicture
    {
        private readonly ILogger _logger;

        private static HttpClient httpClient = new HttpClient();

        public GetDuckPicture(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<GetDuckPicture>();
        }

        [Function("GetDuckPicture")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {

            var getPictureResponse = await httpClient.GetAsync("https://random-d.uk/api/v2/random"); 

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var content = await getPictureResponse.Content.ReadAsStringAsync();
            
            response.WriteString(content);

            return response;
        }
    }
}
