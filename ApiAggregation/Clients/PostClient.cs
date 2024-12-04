using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Services;

namespace ApiAggregationProject.Api.Clients
{
    public class PostClient : IDataService<Post>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PostClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["PostApi:BaseUrl"];
        }
        public async Task<List<Post>> GetDataAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>(_baseUrl);
        }
    }
}
