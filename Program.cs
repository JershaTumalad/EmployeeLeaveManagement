using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        
        static LeaveService service = new LeaveService(new LeaveDBData());

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nWelcome to Employee Leave Management System");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[0] Exit");
                Console.Write("You choose: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Username/Employee ID: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string password = Console.ReadLine();

                        if (int.TryParse(name, out int employeeId))
                            EmployeeLogin(employeeId, password);
                        else
                            AdminLogin(name, password);
                        break;
                    case 0:
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AdminLogin(string username, string password)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Welcome Admin/HR " + username);
                Console.WriteLine("[1] View all Leave Requests");
                Console.WriteLine("[2] Approve / Reject Leave");
                Console.WriteLine("[3] View Employee Points");
                Console.WriteLine("[4] Delete Leave Request");
                Console.WriteLine("[0] Logout");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: ViewAllLeaveRequest(); break;
                    case 2: ApproveOrRejectRequest(); break;
                    case 3: ViewEmployeePoints(); break;
                    case 4: DeleteLeaveRequest(); break;
                    case 0:
                        Console.WriteLine("Logging out...");
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            static void ViewAllLeaveRequest()
            {
                List<LeaveReq> requests = service.GetAllLeaves();
                if (requests.Count == 0) { Console.WriteLine("No leave requests found."); return; }
                Console.WriteLine("\nALL LEAVE REQUESTS");
                foreach (LeaveReq req in requests)
                    Console.WriteLine($"LeaveId: {req.LeaveId} | EmployeeId: {req.EmployeeId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | {req.StartDate.ToShortDateString()} to {req.EndDate.ToShortDateString()} | Status: {req.Status} | Points: {req.PointsDeducted}\n");
            }

            static void ApproveOrRejectRequest()
            {
                bool running = true;
                while (running)
                {
                    List<LeaveReq> requests = service.GetAllLeaves();
                    if (requests.Count == 0) { Console.WriteLine("No leave requests found."); return; }

                    Console.WriteLine("\nALL LEAVE REQUESTS");
                    foreach (LeaveReq req in requests)
                        Console.WriteLine($"LeaveId: {req.LeaveId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | Status: {req.Status}");

                    Console.Write("\nEnter Leave ID to approve/reject (or 'exit' to go back): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "exit") break;

                    if (!Guid.TryParse(input, out Guid leaveId)) { 
                        Console.WriteLine("Invalid ID format."); 
                        continue; }

                    Console.WriteLine("[1] Approve");
                    Console.WriteLine("[2] Reject");
                    Console.Write("Enter your choice: ");
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    string newStatus = choice == 1 ? "Approved" : choice == 2 ? "Rejected" : null;
                    if (newStatus == null) { 
                        Console.WriteLine("Invalid choice."
                     ); continue; }

                    Console.WriteLine(service.UpdateLeave(leaveId, newStatus));
                }
            }

            static void ViewEmployeePoints()
            {
                Console.Write("Enter Employee ID: ");
                if (!int.TryParse(Console.ReadLine(), out int empId))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    return;
                }
                int points = service.GetPoints(empId);
                Console.WriteLine($"Employee {empId} has {points} points remaining.");
            }

            static void DeleteLeaveRequest()
            {
                bool running = true;
                while (running)
                {
                    List<LeaveReq> requests = service.GetAllLeaves();
                    if (requests.Count == 0) { Console.WriteLine("No leave requests found."); return; }

                    Console.WriteLine("\nALL LEAVE REQUESTS");
                    foreach (LeaveReq req in requests)
                        Console.WriteLine($"LeaveId: {req.LeaveId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | Status: {req.Status}");

                    Console.Write("\nEnter Leave ID to delete (or 'exit' to go back): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "exit") break;

                    if (!Guid.TryParse(input, out Guid leaveId)) { Console.WriteLine("Invalid ID format."); continue; }

                    Console.WriteLine(service.DeleteLeave(leaveId));
                }
            }
        }

        static void EmployeeLogin(int employeeId, string password)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("\nWelcome Employee " + employeeId);
                Console.WriteLine("[1] File a Leave Request");
                Console.WriteLine("[2] View My Leave Requests");
                Console.WriteLine("[3] View My Points");
                Console.WriteLine("[0] Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1: 
                        ApplyLeave(employeeId); 
                        break;
                    case 2: 
                        ViewReq(employeeId); 
                        break;
                    case 3: 
                        ViewPoints(employeeId); 
                        break;
                    case 0:
                        Console.WriteLine("Logging out...");
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            static void ApplyLeave(int employeeId)
            {
                Console.Write("\nDo you want to request leave? (Y/N): ");
                char response = Console.ReadLine().ToUpper()[0];

                if (response != 'Y') { 
                    Console.WriteLine("Cancelled."); 
                    return; 
                }

                do
                {
                    Console.Write("Enter Name: ");
                    string employeeName = Console.ReadLine();

                    Console.WriteLine("Leave Types: [1] Vacation  [2] Sick  [3] Emergency");
                    Console.Write("Choose: ");
                    if (!int.TryParse(Console.ReadLine(), out int leaveChoice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }
                    string leaveType = leaveChoice == 1 ? "Vacation" : leaveChoice == 2 ? "Sick" : leaveChoice == 3 ? "Emergency" : null;

                    Console.Write("Enter Start Date (ex. 2025-12-01): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                    {
                        Console.WriteLine("Invalid date format. Please try again.");
                        continue;
                    }

                    Console.Write("Enter End Date (ex. 2025-12-05): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                    {
                        Console.WriteLine("Invalid date format. Please try again.");
                        continue;
                    }

                    Console.Write("Enter Reason: ");
                    string reason = Console.ReadLine();

                    string result = service.ApplyLeave(employeeId, employeeName, leaveType, startDate, endDate, reason);
                    Console.WriteLine(result);

                    Console.Write("\nDo you want to file another leave? (Y/N): ");
                    response = Console.ReadLine().ToUpper()[0];

                } while (response == 'Y');
            }

            static void ViewReq(int employeeId)
            {
                List<LeaveReq> requests = service.GetAllLeaves()
                    .Where(x => x.EmployeeId == employeeId)
                    .ToList();

                if (requests.Count == 0) { Console.WriteLine("No leave requests found."); return; }

                Console.WriteLine("\nMY LEAVE REQUESTS");
                foreach (LeaveReq req in requests)
                {
                    Console.WriteLine("========================");
                    Console.WriteLine($"Type        : {req.LeaveType}");
                    Console.WriteLine($"Start Date  : {req.StartDate.ToShortDateString()}");
                    Console.WriteLine($"End Date    : {req.EndDate.ToShortDateString()}");
                    Console.WriteLine($"Status      : {req.Status}");
                    Console.WriteLine($"Points      : {req.PointsDeducted}");
                    Console.WriteLine("========================\n");
                }
            }

            static void ViewPoints(int empId)
            {
                int points = service.GetPoints(empId);
                Console.WriteLine($"You have {points} points remaining.");
            }
        }
    }
}