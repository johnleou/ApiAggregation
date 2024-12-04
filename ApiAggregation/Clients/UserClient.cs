using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Services;

namespace ApiAggregationProject.Api.Clients
{
    public class UserClient : IDataService<User>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["UserApi:BaseUrl"];
        }

        public async Task<List<User>> GetDataAsync()
        {
            var cachedUsers = await _httpClient.GetFromJsonAsync<List<User>>(_baseUrl);
            return cachedUsers;
        }
    }
}
