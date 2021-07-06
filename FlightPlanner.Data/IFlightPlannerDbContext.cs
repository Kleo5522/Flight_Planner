using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Data
{
    public interface IFlightPlannerDbContext
    {
        DbSet<Flight> Flights { get; set; }
        DbSet<Airport> Airports { get; set; }
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        Task<int> SaveChangesAsync();
    }
}
