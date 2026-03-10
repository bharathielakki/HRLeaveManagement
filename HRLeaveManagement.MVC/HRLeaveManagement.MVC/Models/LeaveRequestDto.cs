namespace HRLeaveManagement.MVC.Models
{
    public class LeaveRequestDto
    {
        public int RequestId { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int BusinessDays { get; set; }
        public string Status { get; set; }
        public string ManagerComment { get; set; }
        public int DeptId { get; set; }
        
    }
}
