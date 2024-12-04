using ApiAggregationProject.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiAggregationProject.Api.Controllers
{
    [ApiController]
    [Route("api/aggregate")]
    public class AggregateController : ControllerBase
    {
        private readonly IAggregationService _aggregationService;

        public AggregateController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDataAsync()
        {
            var response = await _aggregationService.GetAggregatedData();
            return Ok(response);
        }
    }
}
