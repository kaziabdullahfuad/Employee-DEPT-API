namespace EmployeeManagementApi.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        Task<int> CompleteAsync();
    }
}