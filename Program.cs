using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        //ito yung part na magcconnect and LeaveService at Program/mainclass
        static LeaveService sr = new LeaveService();
        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to Employee Leave Management System");
            Console.WriteLine("[1] Show Leave Types");
            Console.WriteLine("[2] Apply Leave");
            Console.WriteLine("[3] View Requests");
            Console.WriteLine("[4] Update Request");
            Console.WriteLine("[5] Delete Request");
            Console.WriteLine("[0] Exit");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ShowLeaveTypes();
                    break;
                case 2:
                    ApplyLeave();
                    break;
                case 3:
                    ViewReq();
                    break;
                case 4:
                    UpdateReq();
                    break;
                case 5:
                    DeleteReq();
                    break;
                case 0:
                    Console.WriteLine("Exiting the system. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            static void ShowLeaveTypes()
            {
                string[] LeaveTypes = { "Vacation", "Sick", "Emergency" };

                Console.WriteLine("Types: ");
                Console.WriteLine("[1] Vacation");
                Console.WriteLine("[2] Sick");
                Console.WriteLine("[3] Emergency");
                Console.WriteLine("[0] Exit");
                Console.Write("Please select a leave type:");
                int Leave = Convert.ToInt32(Console.ReadLine());

                switch (Leave)
                {
                    case 1:
                        Console.WriteLine("You have selected Vacation leave.");
                        break;
                    case 2:
                        Console.WriteLine("You have selected Sick leave.");
                        break;
                    case 3:
                        Console.WriteLine("You have selected Emergency leave.");
                        break;
                    case 0:
                        Console.WriteLine("Exiting the system.");
                        break;
                }

            }

            static void ApplyLeave()
            {
            Console.WriteLine("Do you want to request leave? (Y/N): ");
            char response = Console.ReadLine().ToUpper()[0];

                if (response == 'Y')
                {
                    Console.WriteLine("Applying for leave...\n");
                    string[] EmployeeID;
                    Console.Write("Enter Employee ID:");
                    string ID = Console.ReadLine();

                    Console.WriteLine("Types: ");
                    Console.WriteLine("[1] Vacation");
                    Console.WriteLine("[2] Sick");
                    Console.WriteLine("[3] Emergency");
                    Console.WriteLine("[0] Exit");
                    Console.Write("Enter the leave type you want to apply for:");
                    int Leave = Convert.ToInt32(Console.ReadLine());

                    string leaveType = "";
                    if (Leave == 1)
                        leaveType = "Vacation";
                    else if (Leave == 2)
                        leaveType = "Sick";
                    else if (Leave == 3)
                        leaveType = "Emergency";
                    else
                    {
                        Console.WriteLine("Invalid leave type");
                        return;
                    }

                    Console.Write("Enter Start Date (ex. 2025-12-01: )");
                    string startDate = Console.ReadLine();

                    Console.Write("Enter End Date (ex. 2025-12-05: )");
                    string endtDate = Console.ReadLine();
                }
                else if (response == 'N')
                {
                    Console.WriteLine("Exiting the system.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                }


            }

            static void ViewReq()
            {
                Console.WriteLine("Viewing all leave requests...");
            }

            static void UpdateReq()
            {
                Console.WriteLine();
            }

            static void DeleteReq()
            {
                Console.WriteLine();
            }
        }
    }
}
