
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return NotFound();
            return Ok(department);
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> SearchByName(string name)
        {
            var departments = await _unitOfWork.Departments.SearchByNameAsync(name);
            return Ok(departments);
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create(Department department)
        {
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name
            };
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, departmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            var updated = await _unitOfWork.Departments.UpdateAsync(department);
            if (!updated)
                return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _unitOfWork.Departments.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
