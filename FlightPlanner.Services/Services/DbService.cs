using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Service;
using FlightPlanner.Data;

namespace FlightPlanner.Services.Services
{
    public class DbService : IDbService
    {
        protected readonly IFlightPlannerDbContext Context;

        public DbService(IFlightPlannerDbContext context)
        {
            Context = context;
        }

        public IQueryable<T> Query<T>() where T : Entity
        {
            return Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAsync<T>() where T : Entity
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : Entity
        {
            return await Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync<T>(T entity) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : Entity
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }

            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}