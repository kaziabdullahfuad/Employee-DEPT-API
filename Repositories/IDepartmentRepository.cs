using EmployeeManagementApi.Models;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> SearchByNameAsync(string name);
        Task AddAsync(Department department);
        Task<bool> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}