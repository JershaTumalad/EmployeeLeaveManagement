using System.Data;

﻿using System;


namespace EmployeeLeaveManagement
{
    public class LeaveService
    {

        private LeaveRep repository = new LeaveRep();

        private string[] validLeaveTypes = { "Vacation", "Sick", "Emergency" };

       
        public string ApplyLeave(string employeeID, string leaveType, string startDate, string endDate)
        {
            //rule 1
            if (string.IsNullOrEmpty(employeeID))
            {
                return "Error: Employee ID is needed.";
            }
            bool isValidType = false;

            //rule 2
            if(leaveType == "Vacation")
            {
                isValidType = true;
            } else if(leaveType == "Sick")
            {
                isValidType = false;
            } else if (leaveType == "Emergency")
            {
                isValidType = true;
            }
            else
            {
                isValidType = false;
            }

            if (!isValidType)
            {
                return "Error: Leave Type is invalid";
            }

            //rule 3
            if(startDate == " " || startDate == null || endDate == " " || endDate == null)
            {
                return "Error: Start Date and End Date are needed.";
            }

            
            LeaveReq newRequest = new LeaveReq();
            newRequest.EmployeeID = employeeID;
            newRequest.LeaveType = leaveType;
            newRequest.StartDate = startDate;
            newRequest.EndDate = endDate;
            newRequest.Status = "Pending"; 

            
            repository.AddLeave(newRequest);

            return "Success Apply Leave";
        }

        public List<LeaveReq> GetAllLeaves()
        {
            return repository.GetAllLeaves();
        }//getall ay kumukuha lahat ng req

        public string UpdateLeave(int requestID, string newStatus)
        {
            
            if (newStatus != "Approved" && newStatus != "Rejected" && newStatus != "Pending")
            {
                return "Error: Invalid status!";
            }

            //pass sa rep para masave na yung update
            bool isUpdated = repository.UpdateLeave(requestID, newStatus);

            if (isUpdated)
            {
                return "Update Succesful.";
            }

            return "Can not find employee request.";
        }

        public string DeleteLeave(int requestID)
        {
            bool isDeleted = repository.DeleteLeave(requestID);

            if (isDeleted)
            {
                return "succesfully removed.";
            }

            return "Can not find employee request.";

        public bool HasSufficientPoints(int currentPoints, int requiredPoints)
        {
            return currentPoints >= requiredPoints;
        }

        public bool IsValidDate(DateTime date)
        {
            return date >= DateTime.Today;

        }
    }
}