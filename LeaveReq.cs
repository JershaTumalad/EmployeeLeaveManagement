using System;

namespace EmployeeLeaveManagement
{
    public class LeaveReq
    {
        public int RequestID { get; set; } 
        public string EmployeeID { get; set; }
        public string LeaveType { get; set; } 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}