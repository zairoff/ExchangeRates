using ExchangeRates.Abstractions.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public DbSet<T> DbSet => _dbSet ??= _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync(); // since we are using simple and single operation, unitofwork pattern not required
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await DbSet.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}