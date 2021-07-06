using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Data;
using FlightPlanner.Services.Interfaces;

namespace FlightPlanner.Services.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Airport>> SearchAirportsByPhraseAsync(string phrase)
        {
            var findPhrase = phrase.ToUpper().Trim();
            var foundAirport = await Query().Where(a =>
                a.Country.ToUpper().Trim().Contains(findPhrase) ||
                a.City.ToUpper().Trim().Contains(findPhrase)
                || a.AirportName.Contains(findPhrase)).ToListAsync();
            return foundAirport;
        }
    }
}
