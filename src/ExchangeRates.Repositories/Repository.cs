using ExchangeRates.Abstractions.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            // since we are using simple and single operation, unitofwork pattern not required
            await _context.SaveChangesAsync(); 
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}