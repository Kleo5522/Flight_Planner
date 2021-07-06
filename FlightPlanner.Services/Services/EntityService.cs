using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Service;
using FlightPlanner.Data;

namespace FlightPlanner.Services.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public virtual IQueryable<T> Query()
        {
            return Query<T>();
        }

        public virtual Task<IEnumerable<T>> GetAsync()
        {
            return GetAsync<T>();
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return GetByIdAsync<T>(id);
        }

        public virtual Task CreateAsync(T entity)
        {
            return CreateAsync<T>(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return UpdateAsync<T>(entity);
        }

        public virtual Task DeleteAsync(T entity)
        {
            return DeleteAsync<T>(entity);
        }
    }
}