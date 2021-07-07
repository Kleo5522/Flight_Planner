using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using FlightPlanner.Services.Interfaces;
using FlightPlanner.Services.Models.Data_Format;
using FlightPlanner.Services.Models.Requests;

namespace FlightPlanner.Services.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task AddFlightAsync(Flight flight)
        {
            await CreateAsync(flight);
        }

        public async Task<bool> IsFlightExistAsync(Flight flight)
        {
            return await Query().AnyAsync(f =>
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime &&
                f.Carrier == flight.Carrier &&
                f.From.City == flight.From.City &&
                f.From.Country == flight.From.Country &&
                f.From.AirportName == flight.From.AirportName &&
                f.To.City == flight.To.City &&
                f.To.Country == flight.To.Country &&
                f.To.AirportName == flight.To.AirportName);
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task DeleteFlightByIdAsync(int id)
        {
            await GetByIdAsync(id);
        }

        public async Task<PageResult<Flight>> SearchFlightsReturnPageResultAsync(SearchFlightRequest searchFlight)
        {
            var foundFlights = await Query().Where(f =>
                f.To.AirportName == searchFlight.To && f.From.AirportName == searchFlight.From &&
                f.DepartureTime.Substring(0, 10) == searchFlight.DepartureDate).ToArrayAsync();
            var result = new PageResult<Flight>
            {
                Page = foundFlights.Length,
                TotalItems = foundFlights.Length,
                Items = foundFlights
            };

            return result;
        }

        public async Task DeleteAllFlightsAsync()
        {
            Context.Flights.RemoveRange(Context.Flights);
            Context.Airports.RemoveRange(Context.Airports);
            await Context.SaveChangesAsync();
        }
    }
}
    

