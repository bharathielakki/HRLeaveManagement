namespace HRLeaveManagement.MVC.Models
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DeptId { get; set; }
        public int? ManagerId { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
    }
}
