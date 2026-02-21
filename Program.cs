using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        static string[] EmployeeID = new string[3];
        static string[] LeaveReq = new string[3];
        
        static void Main(string[] args)
        {
            

            Console.WriteLine("Welcome to Employee Leave Management System");
            Console.WriteLine("[1] Show Leave Types");
            Console.WriteLine("[2] Apply Leave");
            Console.WriteLine("[3] View Requests");
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
                case 0:
                    Console.WriteLine("Exiting the system. Goodbye!");
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
                Console.WriteLine("Please select a leave type:");
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
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("Applying for leave...");

                    string[] EmployeeID;
                    Console.WriteLine("Enter Employee ID:");
                    string ID = Console.ReadLine();
                    
                    Console.WriteLine("Please select a leave type:");
                    int choice = int.Parse(Console.ReadLine());


                }
            }

            static void ViewReq()
            {
                Console.WriteLine("Viewing all leave requests...");

                
            }
        }
    }
}
