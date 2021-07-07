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
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IValidation _validation;

        public AdminApiController(IFlightService flightService, IMapper mapper, IValidation validation)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validation = validation;
        }

        [Route("admin-api/flights/{id}"), HttpGet]
        public async Task<IHttpActionResult> GetFlightAsync(int id)
        {
            var flight = await _flightService.GetByIdAsync(id);
            var response = _mapper.Map(flight, new FlightResponse());
            return response == null ? (IHttpActionResult) NotFound() : Ok(response);
        }

        [Route("admin-api/flights"), HttpPut]
        public async Task<IHttpActionResult> CreateNewFlightAsync(AddFlightRequest flight)
        {
            if (_validation.IsAddFlightRequestAnyPropertyContainsNullOrEmptyValue(flight) ||
                _validation.IsAirportToAndAirportFromEqual(flight.To.Airport, flight.From.Airport) ||
                _validation.IsAddFlightRequestArrivalDateTimeLessOrEqualToDepartureDateTime(flight.DepartureTime,
                    flight.ArrivalTime)) return BadRequest();
            var newFlight = _mapper.Map<Flight>(flight);
            if (await _flightService.IsFlightExistAsync(newFlight)) return Conflict();
            await _flightService.CreateAsync(newFlight);
            return Created("", _mapper.Map(newFlight, new FlightResponse()));
        }

        [Route("admin-api/flights/{id}"), HttpDelete]
        public async Task<IHttpActionResult> DeleteFlightAsync(int id)
        {
            var foundFlight = await _flightService.GetFlightByIdAsync(id);
            if (foundFlight != null) await _flightService.DeleteAsync(foundFlight);
            return Ok();
        }
    }
}