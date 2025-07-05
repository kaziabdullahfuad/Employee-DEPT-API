namespace EmployeeManagementApi.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int DeptId { get; set; }
        public DepartmentDto? Department { get; set; }
    }
}