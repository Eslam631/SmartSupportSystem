using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
    
       IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;

        IDepartmentRepository DepartmentRepository { get; }

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken =default);
    }
}
