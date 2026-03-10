using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRLeaveManagement.Domain.Entities
{
    public class Employee 
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
