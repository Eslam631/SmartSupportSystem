using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.DepartmentDto;

namespace Services
{
    public class DepartmentService(IUnitOfWork _unitOfWork,UserManager<ApplicationUser> _userManager) : IDepartmentService
    {
        public async Task<IEnumerable<DepartmentResponse>> GetDepartmentsAsync(CancellationToken cancellation = default)
        {
          var departments=await  _unitOfWork.GenericRepository<Department>().GetAllAsync(cancellation);

            var departmentsDto = departments.Select(x => new DepartmentResponse
            {
                Id = x.Id,
                Name = x.Name,
            });
            return departmentsDto;
        }
        public async Task<DepartmentDetails?> GetDepartmentByIdAsync(Guid id, CancellationToken cancellation = default)
        {
            var department = await _unitOfWork.GenericRepository<Department>().GetByIdAsync(id, cancellation);
            if (department == null || department.IsDeleted)
                throw new DepartmentNotFound(id); // Department not found or deleted
            var CreateUser = await _userManager.FindByIdAsync(department.CreatedBy);
            ApplicationUser? UpdateUser=null;
      if (department.UpdatedBy is not null)
            {
                UpdateUser = await _userManager.FindByIdAsync(department.UpdatedBy);
            }


            var departmentDetails = new DepartmentDetails()
            {
                Id = department.Id,
                Name = department.Name,
                CreatedBy = CreateUser?.UserName! ,
                UpdatedBy = UpdateUser?.UserName ,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt

            };
            return departmentDetails;



        }

        public async Task<DepartmentResponse> CreateDepartmentAsync(DepartmentRequest request,string UserId, CancellationToken cancellation = default)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedBy = UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = null
            };

          await  _unitOfWork.GenericRepository<Department>().AddAsync(department,cancellation);
            var result = await _unitOfWork.SaveChangesAsync(cancellation);
            if (!result)
                throw new Exception($"This Department ={request.Name} is Found");
            return 
                new DepartmentResponse
                 {
                     Id = department.Id,
                     Name = department.Name
                 } ;
               
        }
        public async Task<bool> UpdateDepartmentAsync(Guid id, DepartmentRequest request,string UserId, CancellationToken cancellation = default)
        {
            var department =await _unitOfWork.GenericRepository<Department>().GetByIdAsync(id,cancellation);
            if (department == null || department.IsDeleted)
                throw new DepartmentNotFound(id); // Department not found or deleted

            department.Name = request.Name;
            department.UpdatedBy = UserId;
            department.UpdatedAt = DateTime.UtcNow;
          _unitOfWork.GenericRepository<Department>().Update(department);
          var Result=   await _unitOfWork.SaveChangesAsync();

            return Result;



        }

        public async Task<bool> DeleteDepartmentAsync(Guid id, CancellationToken cancellation = default)
        {
            var department = await _unitOfWork.GenericRepository<Department>().GetByIdAsync(id,cancellation);
            if (department == null || department.IsDeleted)
                throw new DepartmentNotFound(id);
            await _unitOfWork.GenericRepository<Department>().DeleteAsync(id,cancellation);
            var Result = await _unitOfWork.SaveChangesAsync(cancellation);


            return Result;
        }

       

    }
}
