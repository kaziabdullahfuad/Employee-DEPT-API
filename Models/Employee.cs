namespace EmployeeManagementApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int DeptId { get; set; }
        public Department? Department { get; set; }
    }
}