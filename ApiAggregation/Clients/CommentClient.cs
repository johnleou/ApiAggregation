using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Services;

namespace ApiAggregationProject.Api.Clients
{
    public class CommentClient : IDataService<Comment>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CommentClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["CommentApi:BaseUrl"];
        }

        public async Task<List<Comment>> GetDataAsync()
        {
            //var baseUrl = "https://jsonplaceholder.typicode.com/comments";
            return await _httpClient.GetFromJsonAsync<List<Comment>>(_baseUrl);
        }
    }
}
