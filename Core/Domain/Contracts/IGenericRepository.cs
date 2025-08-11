using Domain.Models;

namespace Domain.Contracts
{
   public interface IGenericRepository<T>where T :BaseEntity
    {
      public  Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken=default);
        public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellation = default);

        
        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellation = default);
 


    }
}
