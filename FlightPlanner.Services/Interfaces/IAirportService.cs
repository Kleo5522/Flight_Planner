using System.Collections.Generic;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Service;

namespace FlightPlanner.Services.Interfaces
{
    public interface IAirportService : IEntityService<Airport>
    {
        Task<IEnumerable<Airport>> SearchAirportsByPhraseAsync(string search);
    }
}
