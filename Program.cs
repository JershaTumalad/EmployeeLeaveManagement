using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        static LeaveDataService service = new LeaveDataService();
        static LeaveJson jsonService = new LeaveJson();

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to Employee Leave Management System");
                Console.WriteLine("[1] Login");
                Console.WriteLine("[0] Exit");
                Console.Write("You choose: ");
                Console.Write('\n');
                int choice = Convert.ToInt32(Console.ReadLine());

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
                Console.WriteLine("\nWelcome Admin/HR " + username);
                Console.WriteLine("[1] View all Leave Requests");
                Console.WriteLine("[2] Approve / Reject Leave");
                Console.WriteLine("[3] View Employee Points");
                Console.WriteLine("[4] Delete Leave Request");
                Console.WriteLine("[0] Logout");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

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
                if (requests.Count == 0)
                {
                    Console.WriteLine("No leave requests found.");
                    return;
                }
                Console.WriteLine("\nALL LEAVE REQUESTS");
                foreach (LeaveReq req in requests)
                {
                    Console.WriteLine($"LeaveId: {req.LeaveId} | EmployeeId: {req.EmployeeId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | {req.StartDate.ToShortDateString()} to {req.EndDate.ToShortDateString()} | Status: {req.Status} | Points: {req.PointsDeducted}");
                    Console.Write('\n');
                }
            }

            static void ApproveOrRejectRequest()
            {
                bool running = true;
                while (running)
                {
                    List<LeaveReq> requests = service.GetAllLeaves();
                    if (requests.Count == 0)
                    {
                        Console.WriteLine("No leave requests found.");
                        return;
                    }

                    Console.WriteLine("\nALL LEAVE REQUESTS");
                    foreach (LeaveReq req in requests)
                        Console.WriteLine($"LeaveId: {req.LeaveId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | Status: {req.Status}");

                    Console.Write("\nEnter Leave ID to approve/reject (or 'exit' to go back): ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "exit") break;

                    Guid leaveId = Guid.Parse(input);
                    LeaveReq selected = service.GetById(leaveId);
                    if (selected == null) { Console.WriteLine("Leave request not found."); continue; }

                    Console.WriteLine("[1] Approve");
                    Console.WriteLine("[2] Reject");
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1) { 
                        selected.Status = LeaveStatus.Approved; service.Update(selected); Console.WriteLine("Leave request approved!"); 
                    }else if (choice == 2) { 
                        selected.Status = LeaveStatus.Rejected; service.Update(selected); Console.WriteLine("Leave request rejected!"); 
                    }else Console.WriteLine("Invalid choice.");
                }
            }

            static void DeleteLeaveRequest()
            {
                bool running = true;
                while (running)
                {
                    List<LeaveReq> requests = service.GetAllLeaves();
                    if (requests.Count == 0)
                    {
                        Console.WriteLine("No leave requests found.");
                        return;
                    }

                    Console.WriteLine("\nALL LEAVE REQUESTS");
                    foreach (LeaveReq req in requests)
                        Console.WriteLine($"LeaveId: {req.LeaveId} | Name: {req.EmployeeName} | Type: {req.LeaveType} | Status: {req.Status}");

                    Console.Write("\nEnter Leave ID to delete (or 'exit' to go back): ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "exit") break;

                    Guid leaveId = Guid.Parse(input);
                    LeaveReq selected = service.GetById(leaveId);
                    if (selected == null) { 
                        Console.WriteLine("Leave request not found."); continue; 
                    }

                    service.Delete(leaveId);
                    Console.WriteLine("Leave request deleted successfully!");
                }
            }

            static void ViewEmployeePoints()
            {
                Console.Write("Enter Employee ID: ");
                int empId = Convert.ToInt32(Console.ReadLine());
                int points = service.ViewPoints(empId);
                Console.WriteLine($"Employee {empId} has {points} points remaining.");
            }

        }

        static void EmployeeLogin(int employeeId, string password)
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.WriteLine("Welcome Employee " + employeeId);
                Console.WriteLine("[1] File a Leave Request");
                Console.WriteLine("[2] View My Leave Requests");
                Console.WriteLine("[3] View My Points");
                Console.WriteLine("[0] Exit");
                Console.Write("Enter your choice: ");
                Console.Write('\n');
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1: ApplyLeave(); break;
                    case 2: ViewReq(); break;
                    case 3: ViewPoints(employeeId); break;
                    case 0:
                        Console.WriteLine("Logging out...");
                        loggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            static void ApplyLeave()
            {
                Console.Write("\nDo you want to request leave? (Y/N): ");
                char response = Console.ReadLine().ToUpper()[0];
                Console.Write('\n');

                if (response == 'Y')
                {
                    do
                    {
                        Console.Write("Enter Employee ID: ");
                        int employeeID = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        string employeeName = Console.ReadLine();

                        Console.WriteLine("Leave Types: ");
                        Console.WriteLine("[1] Vacation Leave");
                        Console.WriteLine("[2] Sick Leave");
                        Console.WriteLine("[3] Emergency Leave");
                        Console.Write("Enter the leave type you want to apply for: ");
                        int leave = Convert.ToInt32(Console.ReadLine());

                        LeaveType leaveType;
                        if (leave == 1)
                            leaveType = LeaveType.Vacation;
                        else if (leave == 2)
                            leaveType = LeaveType.Sick;
                        else if (leave == 3)
                            leaveType = LeaveType.Emergency;
                        else
                        {
                            Console.WriteLine("Invalid leave type!");
                            continue;
                        }

                        Console.Write("Enter Start Date (ex. 2025-12-01): ");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("Enter End Date (ex. 2025-12-05): ");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("Enter Reason: ");
                        string reason = Console.ReadLine();

                        Console.WriteLine("\nLEAVE REQUEST SUMMARY");
                        Console.WriteLine($"Employee ID   : {employeeID}");
                        Console.WriteLine($"Employee Name : {employeeName}");
                        Console.WriteLine($"Leave Type    : {leaveType}");
                        Console.WriteLine($"Start Date    : {startDate.ToShortDateString()}");
                        Console.WriteLine($"End Date      : {endDate.ToShortDateString()}");
                        Console.WriteLine($"Reason        : {reason}");

                        try
                        {
                            service.ApplyLeave(employeeID, employeeName, leaveType, startDate, endDate, reason);
                            Console.WriteLine("\nLeave request submitted successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        Console.Write("\nDo you want to file another leave? (Y/N): ");
                        response = Console.ReadLine().ToUpper()[0];

                    } while (response == 'Y');
                    Console.WriteLine("Exiting...");
                }
                else if (response == 'N')
                {
                    Console.WriteLine("Exiting the system.\n");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.\n");
                }
            }
            static void ViewReq()
            {
                List<LeaveReq> requests = service.GetAllLeaves();

                if (requests.Count == 0)
                {
                    Console.WriteLine("No leave requests found.\n");
                    return;
                }

                Console.WriteLine("\nMY LEAVE REQUESTS");
                foreach (LeaveReq req in requests)
                {
                    Console.WriteLine("\n========================");
                    Console.WriteLine($"Type        : {req.LeaveType}");
                    Console.WriteLine($"Start Date  : {req.StartDate.ToShortDateString()}");
                    Console.WriteLine($"End Date    : {req.EndDate.ToShortDateString()}");
                    Console.WriteLine($"Status      : {req.Status}");
                    Console.WriteLine($"Points      : {req.PointsDeducted}");
                    Console.WriteLine("========================");
                    Console.Write('\n');
                }
            }

            static void ViewPoints(int employeeId)
            {
                int points = service.ViewPoints(employeeId);
                Console.WriteLine($"You have {points} points remaining.\n");
            }
        }
    }
}