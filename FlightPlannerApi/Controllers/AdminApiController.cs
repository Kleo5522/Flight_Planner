using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Services.Interfaces;
using FlightPlanner.Services.Models.Requests;
using FlightPlanner.Services.Models.Responses;
using FlightPlannerApi.Attributes;

namespace FlightPlannerApi.Controllers
{
    [@Authorize]
    public class AdminApiController : ApiController
    {
        public IFlightService FlightService;
        public readonly IMapper Mapper;
        public readonly IValidation Validation;

        public AdminApiController(IFlightService flightService, IMapper mapper, IValidation validation)
        {
            FlightService = flightService;
            Mapper = mapper;
            Validation = validation;
        }

        [Route("admin-api/flights/{id}"), HttpGet]
        public async Task<IHttpActionResult> GetFlightAsync(int id)
        {
            var flight = await FlightService.GetByIdAsync(id);
            var response = Mapper.Map(flight, new FlightResponse());
            return response == null ? (IHttpActionResult) NotFound() : Ok(response);
        }

        [Route("admin-api/flights"), HttpPut]
        public async Task<IHttpActionResult> CreateNewFlightAsync(AddFlightRequest flight)
        {
            if (Validation.IsAddFlightRequestAnyPropertyContainsNullOrEmptyValue(flight) ||
                Validation.IsAirportToAndAirportFromEqual(flight.To.Airport, flight.From.Airport) ||
                Validation.IsAddFlightRequestArrivalDateTimeLessOrEqualToDepartureDateTime(flight.DepartureTime,
                    flight.ArrivalTime)) return BadRequest();
            var newFlight = Mapper.Map<Flight>(flight);
            if (await FlightService.IsFlightExistAsync(newFlight)) return Conflict();
            await FlightService.CreateAsync(newFlight);
            return Created("", Mapper.Map(newFlight, new FlightResponse()));
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public async Task<IHttpActionResult> DeleteFlightAsync(int id)
        {
            var foundFlight = await FlightService.GetFlightByIdAsync(id);
            if (foundFlight != null) await FlightService.DeleteAsync(foundFlight);
            return Ok();
        }
    }
}