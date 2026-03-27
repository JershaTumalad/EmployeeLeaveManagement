using System;
namespace EmployeeLeaveManagement
{
    public class LeaveRep
    {
        private List<LeaveReq> leaveRequests = new List<LeaveReq>();
        private LeaveDBData leaveJson = new LeaveDBData();

        public void AddLeave(LeaveReq request)
        {
            leaveRequests.Add(request);
            leaveJson.AddLeave(request);
        }
        public List<LeaveReq> GetAllLeaves()
        {
            return leaveJson.GetAllLeaves();
        }
        public LeaveReq GetLeaveByID(string requestID)
        {
            foreach (LeaveReq request in leaveRequests)
            {
                if (request.RequestID == requestID)
                {
                    return request;
                }
            }
            return null;
        }

        public bool UpdateLeave(string requestID, string newStatus)
        {
            return leaveJson.UpdateLeave(requestID, newStatus);
        }

        public bool DeleteLeave(string requestID)
        {
            return leaveJson.DeleteLeave(requestID);
        }
    }
}