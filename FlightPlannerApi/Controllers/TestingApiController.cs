using System.Threading.Tasks;
using System.Web.Http;
using FlightPlanner.Data;
using FlightPlanner.Services.Interfaces;

namespace FlightPlannerApi.Controllers
{
    public class TestingApiController : ApiController
    {
        private readonly IFlightService _flightService;
        public TestingApiController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [Route("testing-api/clear")]
        [HttpPost]
        public async Task<IHttpActionResult> ClearAsync()
        {
            await _flightService.DeleteAllFlightsAsync();
            return Ok();
        }
    }
}