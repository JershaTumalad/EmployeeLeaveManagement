using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        static string[] EmployeeID = new string[3];
        static string[] LeaveReq = new string[3];
        static int trial = 0;

        static void Main(string[] args)
        {


            Console.WriteLine("Welcome to Employee Leave Management System");
            Console.WriteLine("[1] Show Leave Types");
            Console.WriteLine("[2] Apply Leave");
            Console.WriteLine("[3] View Requests");
            Console.WriteLine("[4] Update Request");
            Console.WriteLine("[5] Delete Request");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("Enter your choice: ");
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

                switch (Leave) {
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

                Console.WriteLine("\nDo you want to request leave? (Y/N): ");
                char response = Console.ReadLine().ToUpper()[0];

                if (response == 'Y')
                {
                    Console.WriteLine("Applying for leave...\n");
                    string[] EmployeeID;
                    Console.WriteLine("Enter Employee ID:");
                    string ID = Console.ReadLine();

                    Console.WriteLine("Types: ");
                    Console.WriteLine("[1] Vacation");
                    Console.WriteLine("[2] Sick");
                    Console.WriteLine("[3] Emergency");
                    Console.WriteLine("[0] Exit");
                    Console.Write("Enter the leave type you want to apply for:");
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
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
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

                Console.WriteLine("Employee ID\tLeave Type");
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
