using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Service
{
    public interface IDbService
    {
        IQueryable<T> Query<T>() where T : Entity;
        Task<IEnumerable<T>> GetAsync<T>() where T : Entity;
        Task<T> GetByIdAsync<T>(int id) where T : Entity;
        Task CreateAsync<T>(T entity) where T : Entity;
        Task UpdateAsync<T>(T entity) where T : Entity;
        Task DeleteAsync<T>(T entity) where T : Entity;
    }
}
