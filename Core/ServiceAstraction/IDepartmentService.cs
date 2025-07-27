using Shared.DepartmentDto;

namespace ServiceAbstraction
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentResponse>> GetDepartmentsAsync(CancellationToken cancellation =default);
        public Task<DepartmentDetails?> GetDepartmentByIdAsync(Guid id, CancellationToken cancellation = default);

        public Task<DepartmentResponse> CreateDepartmentAsync(DepartmentRequest request,string UserId, CancellationToken cancellation = default);
        public Task<bool> UpdateDepartmentAsync(Guid id, DepartmentRequest request,string UserId, CancellationToken cancellation = default);
        public Task<bool> DeleteDepartmentAsync(Guid id, CancellationToken cancellation = default);
    }
}
