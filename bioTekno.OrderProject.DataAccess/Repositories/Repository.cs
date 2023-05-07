using bioTekno.OrderProject.DataAccess.Contexts;
using bioTekno.OrderProject.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly bioTeknoContext _context;

        public Repository(bioTeknoContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return !asNoTracking ? await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter) : await _context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity, T unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }
    }
}
