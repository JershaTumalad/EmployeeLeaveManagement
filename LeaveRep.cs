using System;
namespace EmployeeLeaveManagement
{
    public interface LeaveRep
    {
        void Add(LeaveReq leave);
        LeaveReq? GetById(Guid id);
        List<LeaveReq> GetAll();
        void Edit(LeaveReq leave);
        void Delete(Guid id);
        int GetPoints(int employeeId);
        void UpdatePoints(int employeeId, int points);

        List<LeaveReq> GetLeavesByEmployee(int employeeId);
        List<LeaveReq> GetLeavesByStatus(LeaveStatus status);
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public enum LeaveType
    {
        Sick,
        Vacation,
        Emergency
    }
}