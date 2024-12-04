using ApiAggregationProject.Api.Models;

namespace ApiAggregationProject.Api.Services
{
    public interface IAggregationService
    {
        public Task<AggregatedResponse> GetAggregatedData();
    }
}
