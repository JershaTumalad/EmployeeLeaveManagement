using System;

namespace EmployeeLeaveManagement
{
    public class LeaveRep
    {
        private List<LeaveReq> leaveRequests = new List<LeaveReq>();
        private int lastID = 0; //number para sa nagbibilang/counter

        public void AddLeave(LeaveReq request)
        {
            lastID++;//nagaadd
            request.RequestID = lastID;
            leaveRequests.Add(request);
        }
        public List<LeaveReq> GetAllLeaves()
        {
            return leaveRequests;
        }
        public LeaveReq GetLeaveByID(int requestID)
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

        public bool UpdateLeave(int requestID, string newStatus)
        {
            LeaveReq request = GetLeaveByID(requestID);

            if (request != null)
            {
                request.Status = newStatus;
                return true; 
            }

            return false; 
        }

        public bool DeleteLeave(int requestID)
        {
            LeaveReq request = GetLeaveByID(requestID);

            if (request != null)
            {
                leaveRequests.Remove(request);
                return true; 
            }

            return false; 
        }
    }
}