using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Service;
using FlightPlanner.Services.Models.Data_Format;
using FlightPlanner.Services.Models.Requests;

namespace FlightPlanner.Services.Interfaces
{
    public interface IFlightService : IEntityService<Flight>
    {
        Task AddFlightAsync(Flight flight);
        Task<bool> IsFlightExistAsync(Flight flight);
        Task<Flight> GetFlightByIdAsync(int id);
        Task DeleteFlightByIdAsync(int id);
        Task<PageResult<Flight>> SearchFlightsReturnPageResultAsync(SearchFlightRequest searchFlight);
    }
}
