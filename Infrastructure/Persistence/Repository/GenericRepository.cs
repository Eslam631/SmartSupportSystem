using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Context;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class GenericRepository<T>(ApplicationDbContext _dbContext) : IGenericRepository<T> where T : BaseEntity
    {
      

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
        return  await  _dbContext.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default)
        {
           return await _dbContext.Set<T>().FindAsync(id,cancellation);
        }

        public async Task<bool> AddAsync(T entity, CancellationToken cancellation = default)
        {
         var result=  await _dbContext.Set<T>().AddAsync(entity,cancellation) ;
            if (result == null)
                return false; // Entity not added successfully
            
            return result.State == EntityState.Added? true : false;

        }

        public bool Update(T entity, CancellationToken cancellation = default)
        {
            var result = _dbContext.Set<T>().Update(entity);
            return result.State == EntityState.Modified ? true : false;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellation = default)
        {
            var entity =await GetByIdAsync(id,  cancellation);
            if (entity == null)
                return false; // Entity not found
            
            entity.IsDeleted = true; 
          var Result=  _dbContext.Set<T>().Update(entity);

            return Result.State == EntityState.Modified ? true : false;
        }
    }
}
