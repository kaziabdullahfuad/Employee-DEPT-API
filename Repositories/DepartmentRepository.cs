using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToListAsync();
        }

        public async Task<DepartmentDto?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Where(d => d.Id == id)
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DepartmentDto>> SearchByNameAsync(string name)
        {
            return await _context.Departments
                .Where(d => d.Name.Contains(name))
                .Select(d => new DepartmentDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToListAsync();
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
                return false;

            _context.Entry(existingDepartment).CurrentValues.SetValues(department);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
                return false;

            _context.Departments.Remove(department);
            return true;
        }
    }
}
