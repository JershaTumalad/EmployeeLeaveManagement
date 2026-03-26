using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        //ito yung part na magcconnect ang LeaveServices at Program
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
                Console.WriteLine("\nLEAVE TYPES ");
                Console.WriteLine("[1] Vacation");
                Console.WriteLine("[2] Sick");
                Console.WriteLine("[3] Emergency");

                // iask ni system kung magaapply ba si user ng leave
                Console.Write("\nGusto mo bang mag-apply ng leave? (Y/N): ");
                char response = Console.ReadLine().ToUpper()[0];

                if (response == 'Y')
                {
                    ApplyLeave();
                }
                else if (response == 'N')
                {
                    Console.WriteLine("Exiting program.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter Y or N.");
                }
            }


            static void ApplyLeave()
            {
                Console.Write("\nDo you want to request leave? (Y/N): ");
                char response = Console.ReadLine().ToUpper()[0];

                if (response == 'Y')
                {
                    //do while loop para makaapply ulit si user 
                    do
                    {
                        Console.Write("Enter Employee ID: ");
                        string employeeID = Console.ReadLine();

                        Console.WriteLine("Leave Types: ");
                        Console.WriteLine("[1] Vacation");
                        Console.WriteLine("[2] Sick");
                        Console.WriteLine("[3] Emergency");
                        Console.Write("Enter the leave type you want to apply for: ");
                        int leave = Convert.ToInt32(Console.ReadLine());

                        string leaveType = "";
                        if (leave == 1) 
                            leaveType = "Vacation";
                        else if (leave == 2) 
                            leaveType = "Sick";
                        else if (leave == 3) 
                            leaveType = "Emergency";
                        else
                        {
                            Console.WriteLine("Invalid leave type!");
                            continue; // maglo-loop na dito
                        }

                        Console.Write("Enter Start Date (ex.2025-12-01): ");
                        string startDate = Console.ReadLine();

                        Console.Write("Enter End Date (ex.2025-12-05): ");
                        string endDate = Console.ReadLine();

                        //display
                        Console.WriteLine("\nLEAVE REQUEST");
                        Console.WriteLine($"Employee ID : {employeeID}");
                        Console.WriteLine($"Leave Type  : {leaveType}");
                        Console.WriteLine($"Start Date  : {startDate}");
                        Console.WriteLine($"End Date    : {endDate}");

                        string result = sr.ApplyLeave(employeeID, leaveType, startDate, endDate);
                        Console.WriteLine(result);

                        //tatanungin si user kung gusto pa niya ulit magapply ng leave req
                        Console.Write("\nDo you want to file another leave?(Y/N): ");
                        response = Console.ReadLine().ToUpper()[0];

                    } while (response == 'Y'); // Kapag Y uulit, 'pag N hindi na
                    Console.WriteLine("Exiting the system.");
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
                List<LeaveReq> requests = sr.GetAllLeaves();

                if (requests.Count == 0)
                {
                    Console.WriteLine("No leave requests found.");
                    return;
                }

                Console.WriteLine("\nALL LEAVE REQUESTS");
                foreach (LeaveReq req in requests)
                {
                    Console.WriteLine($"ID: {req.RequestID} | Employee: {req.EmployeeID} | Type: {req.LeaveType} | {req.StartDate} to {req.EndDate} | Status: {req.Status}");
                }
            }

            static void UpdateReq()
            {
                    Console.Write("Enter Request ID to update: ");
                    int requestID = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Select new status:");
                    Console.WriteLine("[1] Approved");
                    Console.WriteLine("[2] Rejected");
                    Console.WriteLine("[3] Pending");
                    Console.Write("Enter your choice: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    string newStatus = "";
                    if (choice == 1) 
                    newStatus = "Approved";
                    else if (choice == 2) 
                    newStatus = "Rejected";
                    else if (choice == 3) 
                    newStatus = "Pending";
                    else
                    {
                        Console.WriteLine("Invalid choice!");
                        return;
                    }

                    string result = sr.UpdateLeave(requestID, newStatus);
                    Console.WriteLine(result);
                }
            }

          
            static void DeleteReq()
            {
                Console.Write("Enter Request ID to delete: ");
                int requestID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Are you sure you want to delete this request? (Y/N): ");
                char confirm = Console.ReadLine().ToUpper()[0];

                if (confirm == 'Y')
                {
                    string result = sr.DeleteLeave(requestID);
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine("Delete cancelled.");
                }
            }
        }
        }
