namespace ApiAggregationProject.Api.Services
{
    public interface IDataService<T>
    {
        public Task<List<T>> GetDataAsync();
    }
}
