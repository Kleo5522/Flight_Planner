using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using FlightPlanner.Services.Interfaces;
using FlightPlanner.Services.Models.Requests;
using FlightPlanner.Services.Models.Responses;

namespace FlightPlannerApi.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        private readonly IValidation _validation;

        public CustomerController(IFlightService flightService, IAirportService airportService, IMapper mapper, IValidation validation)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
            _validation = validation;
        }

        [Route("api/airports"), HttpGet]
        public async Task<IHttpActionResult> SearchAirportsAsync(string search)
        {
            var foundAirports = await _airportService.SearchAirportsByPhraseAsync(search);
            var airportsList = foundAirports.Select(airport => _mapper.Map(airport, new AirportResponse())).ToList();
            return airportsList.Count != 0 ? Ok(airportsList) : (IHttpActionResult) NotFound();
        }

        [Route("api/flights/search"), HttpPost]
        public async Task<IHttpActionResult> SearchFlightsAsync(SearchFlightRequest search)
        {
            if (_validation.IsSearchFlightRequestAnyPropertyContainsNullValue(search) ||
                _validation.IsAirportToAndAirportFromEqual(search.To, search.From)) return BadRequest();
            return Ok(await _flightService.SearchFlightsReturnPageResultAsync(search));
        }

        [Route("api/flights/{id}"), HttpGet]
        public async Task<IHttpActionResult> FindFlights(int id)
        {
            var foundFlight = await _flightService.GetByIdAsync(id);
            var response = _mapper.Map(foundFlight, new FlightResponse());
            return foundFlight == null ? (IHttpActionResult) NotFound() : Ok(response);
        }
    }
}