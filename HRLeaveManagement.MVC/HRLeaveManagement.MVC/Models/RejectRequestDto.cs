namespace HRLeaveManagement.MVC.Models
{
    public class RejectRequestDto
    {
        public int RequestId { get; set; }

        public int ManagerId { get; set; }

        public string Reason { get; set; }
    }
}
