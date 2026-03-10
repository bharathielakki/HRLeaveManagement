namespace HRLeaveManagement.MVC.Models
{
    public class LeaveBalanceDto
    {
        public int LeaveTypeId { get; set; }
        public string LeaveType{ get; set; }
        public int TotalDays { get; set; }
        public int UsedDays { get; set; }

        public int RemainingDays => TotalDays - UsedDays;
    }
}
