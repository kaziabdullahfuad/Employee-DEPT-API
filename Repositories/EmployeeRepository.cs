
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    DeptId = e.DeptId,
                    Department = e.Department != null ? new DepartmentDto
                    {
                        Id = e.Department.Id,
                        Name = e.Department.Name
                    } : null
                })
                .ToListAsync();
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Id == id)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    DeptId = e.DeptId,
                    Department = e.Department != null ? new DepartmentDto
                    {
                        Id = e.Department.Id,
                        Name = e.Department.Name
                    } : null
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> SearchByNameAsync(string name)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.Name.Contains(name))
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    DeptId = e.DeptId,
                    Department = e.Department != null ? new DepartmentDto
                    {
                        Id = e.Department.Id,
                        Name = e.Department.Name
                    } : null
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> SearchByPhoneNumberAsync(string phoneNumber)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.PhoneNumber.Contains(phoneNumber))
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    DeptId = e.DeptId,
                    Department = e.Department != null ? new DepartmentDto
                    {
                        Id = e.Department.Id,
                        Name = e.Department.Name
                    } : null
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> SearchByDepartmentAsync(int deptId)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Where(e => e.DeptId == deptId)
                .Select(e => new EmployeeDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    DeptId = e.DeptId,
                    Department = e.Department != null ? new DepartmentDto
                    {
                        Id = e.Department.Id,
                        Name = e.Department.Name
                    } : null
                })
                .ToListAsync();
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var existingEmployee = await _context.Employees.FindAsync(employee.Id);
            if (existingEmployee == null)
                return false;

            _context.Entry(existingEmployee).CurrentValues.SetValues(employee);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            return true;
        }
    }
}
