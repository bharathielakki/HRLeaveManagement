namespace HRLeaveManagement.MVC.Models
{
    public class TeamCalendarDto
    {
        public int EmpId { get; set; }
        public string Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }

        public string status { get; set; }
    }
}
