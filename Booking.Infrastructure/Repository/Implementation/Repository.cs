using Booking.Domain.Entities.Base;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(T input)
        {
            await _dbContext.Set<T>().AddAsync(input);
        }
        public void Delete(T input)
        {
            _dbContext.Set<T>().Remove(input);
        }
        public void Update(T input)
        {
            _dbContext.Set<T>().Update(input);
        }

        public async Task<IEnumerable<T>> GetCollectionAsync(Expression<Func<T,bool>>? expression = null)
        {
            return expression == null ? await _dbContext.Set<T>().AsNoTracking().ToListAsync() : await _dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public IQueryable<T> GetQuerry(Expression<Func<T,bool>>? expression = null)
        {
            return expression == null ? _dbContext.Set<T>() : _dbContext.Set<T>().Where(expression);
        }

      
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

       

    }
}
