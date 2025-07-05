using Microsoft.EntityFrameworkCore;
using EmployeeManagementApi.Models;

namespace EmployeeManagementApi.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Ensure the database is created
            await context.Database.EnsureCreatedAsync();

            // Check if any departments exist
            if (!await context.Departments.AnyAsync())
            {
                // Seed Departments
                var departments = new List<Department>
                {
                    new Department { Name = "Human Resources" },
                    new Department { Name = "Engineering" },
                    new Department { Name = "Marketing" }
                };
                await context.Departments.AddRangeAsync(departments);
                await context.SaveChangesAsync();
            }

            // Check if any employees exist
            if (!await context.Employees.AnyAsync())
            {
                // Get department IDs
                var departments = await context.Departments.ToListAsync();

                // Seed Employees
                var employees = new List<Employee>
                {
                    new Employee { Name = "John Doe", PhoneNumber = "1234567890", DeptId = departments[0].Id },
                    new Employee { Name = "Jane Smith", PhoneNumber = "0987654321", DeptId = departments[1].Id },
                    new Employee { Name = "Bob Johnson", PhoneNumber = "5551234567", DeptId = departments[2].Id }
                };
                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}