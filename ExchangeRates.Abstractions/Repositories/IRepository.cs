﻿using System.Linq.Expressions;

namespace ExchangeRates.Abstractions.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);

        void Delete(T entity);

        void Update(T entity);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> FindAsync(Expression<Func<T, bool>> expression);
    }
}