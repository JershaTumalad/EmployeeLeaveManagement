using System;

namespace EmployeeLeaveManagement
{
    public class LeaveReq
    {
        public Guid LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PointsDeducted { get; set; }
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
        public string Reason { get; set; } = string.Empty;
        public int LeaveDays => (EndDate - StartDate).Days + 1;
    }

    public class EmployeePoint
    {
        public int EmployeeId { get; set; }
        public int Points { get; set; }
    }
}