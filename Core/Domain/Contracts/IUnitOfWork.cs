using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
    
        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
   
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken =default);
    }
}
