using BlazorApp.Shared;

namespace BlazorApp.Client.Services.Interfaces
{
    public interface IMemeNewsService
    {
        Task<List<MemeNews>> GetMemeNews();
    }
}
