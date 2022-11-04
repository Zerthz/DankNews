using BlazorApp.Client.Services.Interfaces;
using BlazorApp.Shared;
using System.Net.Http.Json;

namespace BlazorApp.Client.Services
{
    public class MemeNewsService : IMemeNewsService
    {
        private readonly HttpClient _http;

        public MemeNewsService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MemeNews>> GetMemeNews()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<List<MemeNews>>("/api/GetMemeNews");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }
    }
}
