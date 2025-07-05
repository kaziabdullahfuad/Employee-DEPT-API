using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Repositories;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetById(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }

        [HttpGet("search/name/{name}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchByName(string name)
        {
            var employees = await _unitOfWork.Employees.SearchByNameAsync(name);
            return Ok(employees);
        }

        [HttpGet("search/phone/{phoneNumber}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchByPhoneNumber(string phoneNumber)
        {
            var employees = await _unitOfWork.Employees.SearchByPhoneNumberAsync(phoneNumber);
            return Ok(employees);
        }

        [HttpGet("search/department/{deptId}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> SearchByDepartment(int deptId)
        {
            var employees = await _unitOfWork.Employees.SearchByDepartmentAsync(deptId);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Create(Employee employee)
        {
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                DeptId = employee.DeptId
            };
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            var updated = await _unitOfWork.Employees.UpdateAsync(employee);
            if (!updated)
                return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _unitOfWork.Employees.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}