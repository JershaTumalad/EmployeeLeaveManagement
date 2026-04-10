using System;

namespace EmployeeLeaveManagement
{


    public class LeaveDataService
    {
        private LeaveDBData _db;

        public LeaveDataService()
        {
            _db = new LeaveDBData();
        }

        public int ViewPoints(int employeeId)
        {
            return _db.GetPoints(employeeId);
        }

        public  List<LeaveReq> GetAllLeaves()
        {
            return _db.GetAll();
        }

        public void ViewRequests()
        {
            List<LeaveReq> requests = _db.GetAll();

            foreach (LeaveReq leave in requests)
            {
                Console.WriteLine($"LeaveId: {leave.LeaveId}");
                Console.WriteLine($"Employee: {leave.EmployeeName}");
                Console.WriteLine($"Employee ID: {leave.EmployeeId}");
                Console.WriteLine($"Type of Leave: {leave.LeaveType}");
                Console.WriteLine($"From: {leave.StartDate}");
                Console.WriteLine($"Until: {leave.EndDate}");
                Console.WriteLine($"Points Deducted: {leave.PointsDeducted}");
                Console.WriteLine($"Reason: {leave.Reason}");
            }
        }

        public void ApplyLeave(int EmployeeId, string employeeName, LeaveType leaveType, DateTime startDate, DateTime endDate, string reason)
        {
            int days = (endDate - startDate).Days;
            int currentPoints = ViewPoints(EmployeeId);

            if (currentPoints >= days)
            {
                LeaveReq newLeave = new LeaveReq
                {
                    LeaveId = Guid.NewGuid(),
                    EmployeeId = EmployeeId,
                    EmployeeName = employeeName,
                    LeaveType = leaveType,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = LeaveStatus.Pending,
                    PointsDeducted = days,
                    Reason = reason
                };
                _db.Add(newLeave);

                _db.UpdatePoints(EmployeeId, currentPoints - days);
            }
            else
            {
                Console.WriteLine("Not enough points to apply for leave!");
            }
        }

        public LeaveReq? GetById(Guid id)
        {
            return _db.GetById(id);
        }

        public void Update(LeaveReq leave)
        {
            _db.Edit(leave);
        }

        public void Delete(Guid id)
        {
            _db.Delete(id);
        }
    }
}