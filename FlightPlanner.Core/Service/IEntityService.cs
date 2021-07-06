using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Service
{
    public interface IEntityService<T> where T : Entity
    {
        IQueryable<T> Query();
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
