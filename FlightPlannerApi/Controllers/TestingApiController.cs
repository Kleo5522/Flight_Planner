using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FlightPlanner.Data;
using WebGrease.Css.Extensions;

namespace FlightPlannerApi.Controllers
{
    public class TestingApiController : ApiController
    {
        [Route("testing-api/clear")]
        [HttpPost]
        public async Task<IHttpActionResult> ClearAsync()
        {
            using (var context = new FlightPlannerDbContext())
            {
                context.Flights.RemoveRange(context.Flights);
                context.Airports.RemoveRange(context.Airports);
                await context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}