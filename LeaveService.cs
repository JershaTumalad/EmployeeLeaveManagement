using System;

namespace EmployeeLeaveManagement
{
    public class LeaveService
    {
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