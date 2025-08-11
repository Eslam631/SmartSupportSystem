using Domain.Models;

namespace Domain.Contracts
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        public  Task<bool> AddAsync(Department entity, CancellationToken cancellation = default);
        public Task<bool> Update(Department entity, CancellationToken cancellation = default);
    }
}
