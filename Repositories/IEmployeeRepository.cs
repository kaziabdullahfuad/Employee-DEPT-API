using EmployeeManagementApi.Models;
using EmployeeManagementApi.DTOs;

namespace EmployeeManagementApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> SearchByNameAsync(string name);
        Task<IEnumerable<EmployeeDto>> SearchByPhoneNumberAsync(string phoneNumber);
        Task<IEnumerable<EmployeeDto>> SearchByDepartmentAsync(int deptId);
        Task AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int id);
    }
}