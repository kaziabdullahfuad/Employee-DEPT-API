
using EmployeeManagementApi.Data;

namespace EmployeeManagementApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IEmployeeRepository Employees { get; private set; }
        public IDepartmentRepository Departments { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
