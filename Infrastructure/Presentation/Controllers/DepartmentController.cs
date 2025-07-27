using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DepartmentDto;
using System.Security.Claims;

namespace Presentation.Controllers
{
   public class DepartmentController(IServiceManager _serviceManager):ApiBaseController
    {
       
        [HttpGet("GetAll")]
        public async Task<ActionResult<DepartmentResponse>> GetAllDepartments(CancellationToken cancellationToken=default)
        {
            var departments = await _serviceManager.DepartmentService.GetDepartmentsAsync(cancellationToken);
            return Ok(departments);
        }
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DepartmentDetails>> GetDepartmentById(Guid id, CancellationToken cancellation = default)
        {
            var department = await _serviceManager.DepartmentService.GetDepartmentByIdAsync(id,cancellation);
           
            return Ok(department);
        }
        [HttpPost("Create")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<DepartmentResponse>> CreateDepartment([FromBody] DepartmentRequest request, CancellationToken cancellation = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var department = await _serviceManager.DepartmentService.CreateDepartmentAsync(request, userId!, cancellation);
            return Ok(department);
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> UpdateDepartment([FromRoute]Guid id, [FromBody] DepartmentRequest request, CancellationToken cancellation = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _serviceManager.DepartmentService.UpdateDepartmentAsync(id, request, userId!, cancellation);
           return Ok(result);
        }
        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteDepartment(Guid id, CancellationToken cancellation = default)
        {
            var result = await _serviceManager.DepartmentService.DeleteDepartmentAsync(id, cancellation);
            return Ok(result);
        }
    }
}
