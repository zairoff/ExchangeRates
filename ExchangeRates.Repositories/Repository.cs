using ExchangeRates.Abstractions.Repositories;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public DbSet<T> DbSet => _dbSet ??= _context.Set<T>();

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}