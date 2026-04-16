using System;
namespace EmployeeLeaveManagement
{
    public class LeaveService
    {
        private readonly LeaveRep _repo;

        public LeaveService(LeaveRep repo)
        {
            _repo = repo;
        }

        public string ApplyLeave(int employeeId, string employeeName, string leaveType, DateTime startDate, DateTime endDate, string reason)
        {
            if (startDate < DateTime.Today)
                return "Invalid dates. Start date cannot be in the past.";

            if (endDate < startDate)
                return "Invalid dates. End date cannot be earlier than start date.";

            int leaveDays = (endDate - startDate).Days + 1;
            int pointsNeeded = leaveType switch
            {
                "Vacation" => leaveDays * 2,
                "Sick" => leaveDays * 1,
                "Emergency" => leaveDays * 1,
                _ => leaveDays * 1
            };

            int currentPoints = _repo.GetPoints(employeeId);
            if (currentPoints < pointsNeeded)
                return "Insufficient points!";

            var leave = new LeaveReq
            {
                LeaveId = Guid.NewGuid(),
                EmployeeId = employeeId,
                EmployeeName = employeeName,
                LeaveType = (LeaveType)Enum.Parse(typeof(LeaveType), leaveType),
                StartDate = startDate,
                EndDate = endDate,
                PointsDeducted = pointsNeeded,
                Status = LeaveStatus.Pending,
                Reason = reason
            };

            _repo.Add(leave);
            
            return "Leave filed successfully!";
        }

        public List<LeaveReq> GetAllLeaves()
        {
            return _repo.GetAll();
        }

        public LeaveReq? GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public string UpdateLeave(Guid requestId, string newStatus)
        {
            var leave = _repo.GetById(requestId);
            if (leave == null)
                return "Request not found. Try again.";

            LeaveStatus parsedStatus = (LeaveStatus)Enum.Parse(typeof(LeaveStatus), newStatus);

            if (parsedStatus == LeaveStatus.Approved && leave.Status == LeaveStatus.Pending)
            {
                int currentPoints = _repo.GetPoints(leave.EmployeeId);
                if (currentPoints < leave.PointsDeducted)
                    return "Employee has insufficient points!";
                _repo.UpdatePoints(leave.EmployeeId, currentPoints - leave.PointsDeducted);
            }

            if (parsedStatus == LeaveStatus.Rejected && leave.Status == LeaveStatus.Approved)
            {
                int currentPoints = _repo.GetPoints(leave.EmployeeId);
                _repo.UpdatePoints(leave.EmployeeId, currentPoints + leave.PointsDeducted);
            }

            leave.Status = parsedStatus;
            _repo.Edit(leave);
            return $"Leave {newStatus} successfully.";
        }
        

        public string DeleteLeave(Guid requestId)
        {
            var leave = _repo.GetById(requestId);
            if (leave == null)
                return "Request not found. Try again.";

            _repo.Delete(leave.LeaveId);
            return "Delete successful.";
        }

        public int GetPoints(int employeeId)
        {
            return _repo.GetPoints(employeeId);
        }

        public List<LeaveReq> GetLeavesByEmployee(int employeeId)
            => _repo.GetLeavesByEmployee(employeeId);

        public List<LeaveReq> GetLeavesByStatus(LeaveStatus status)
            => _repo.GetLeavesByStatus(status);

    }
}