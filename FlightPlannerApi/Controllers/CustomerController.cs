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
        public IFlightService FlightService;  //nevajag publiskos Servisus????
        public IAirportService AirportService;
        public readonly IMapper Mapper;
        public readonly IValidation Validation;

        public CustomerController(IFlightService flightService, IAirportService airportService, IMapper mapper, IValidation validation)
        {
            FlightService = flightService;
            AirportService = airportService;
            Mapper = mapper;
            Validation = validation;
        }

        [Route("api/airports"), HttpGet]
        public async Task<IHttpActionResult> SearchAirportsAsync(string search)
        {
            var foundAirports = await AirportService.SearchAirportsByPhraseAsync(search);
            var airportsList = foundAirports.Select(airport => Mapper.Map(airport, new AirportResponse())).ToList();
            return airportsList.Count != 0 ? Ok(airportsList) : (IHttpActionResult) NotFound();
        }

        [Route("api/flights/search"), HttpPost]
        public async Task<IHttpActionResult> SearchFlightsAsync(SearchFlightRequest search)
        {
            if (Validation.IsSearchFlightRequestAnyPropertyContainsNullValue(search) ||
                Validation.IsAirportToAndAirportFromEqual(search.To, search.From)) return BadRequest();
            return Ok(await FlightService.SearchFlightsReturnPageResultAsync(search));
        }

        [Route("api/flights/{id}"), HttpGet]
        public async Task<IHttpActionResult> FindFlights(int id)
        {
            var foundFlight = await FlightService.GetByIdAsync(id);
            var response = Mapper.Map(foundFlight, new FlightResponse());
            return foundFlight == null ? (IHttpActionResult) NotFound() : Ok(response);
        }
    }
}