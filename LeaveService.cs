using System;
namespace EmployeeLeaveManagement
{
    public class LeaveService
    {
        private readonly LeaveDataService _LeaveDataService;

        public LeaveService()
        {
            _LeaveDataService = new LeaveDataService();
        }

        public string ApplyLeave(string employeeID, string leaveType, string startDate, string endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            if (start < DateTime.Today)
                return "Invalid dates. Start date can not be in the past.";

            if (end < start)
                return "Invalid dates. End date can not be earlier than start date";

            int leaveDays = (end - start).Days + 1;
            int pointsNeeded = 0;

            if (leaveType == "Vacation")
                pointsNeeded = leaveDays * 2;
            else if (leaveType == "Sick")
                pointsNeeded = leaveDays * 1;
            else if (leaveType == "Emergency")
                pointsNeeded = leaveDays * 1;
            else if (leaveType == "Maternity")
                pointsNeeded = leaveDays * 3;

            int currentPoints = _LeaveDataService.ViewPoints(int.Parse(employeeID));
            if (currentPoints < pointsNeeded)
                return "Insufficient points!";

            var leave = new LeaveReq
            {
                LeaveId = Guid.NewGuid(),
                EmployeeId = int.Parse(employeeID),
                LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), leaveType),
                StartDate = start,
                EndDate = end,
                PointsDeducted = pointsNeeded,
                Status = LeaveStatus.Pending
            };

            _LeaveDataService.ApplyLeave(
                leave.EmployeeId,
                leave.EmployeeName,
                leave.LeaveType,
                leave.StartDate,
                leave.EndDate,
                leave.Reason
            );

            return "Leave filed successfully!";
        }

        public List<LeaveReq> GetAllLeaves()
        {
            return _LeaveDataService.GetAllLeaves();
        }

        public string UpdateLeave(string requestId, string newStatus)
        {
            var leave = _LeaveDataService.GetById(Guid.Parse(requestId));
            if (leave == null)
                return "Request not found. Try again.";

            leave.Status = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), newStatus);
            _LeaveDataService.Update(leave);
            return "Change successful.";
        }

        public string DeleteLeave(string requestId)
        {
            var leave = _LeaveDataService.GetById(Guid.Parse(requestId));
            if (leave == null)
                return "Request not found. Try again.";

            _LeaveDataService.Delete(leave.LeaveId);
            return "Delete successful.";
        }

        public int GetPoints(int employeeId)
        {
            return _LeaveDataService.ViewPoints(employeeId);
        }
    }
}