using System.Data.Entity;
using FlightPlanner.Core.Models;
using FlightPlanner.Data.Migrations;

namespace FlightPlanner.Data
{
    public class FlightPlannerDbContext : DbContext, IFlightPlannerDbContext
    {
        public FlightPlannerDbContext() : base("FlightPlannerSolidDb")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<
                    FlightPlannerDbContext, Configuration
                >());
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
