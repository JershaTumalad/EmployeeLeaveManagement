using System;

namespace EmployeeLeaveManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] LeaveTypes = {"Vacation", "Sick", "Emergency"};

            Console.WriteLine("Welcome to Employee Leave Management System");
            Console.WriteLine("Please enter your employee ID:");
            Console.ReadLine();

            Console.WriteLine("Types: Vacation , Sick, Emergency");
            Console.WriteLine("Please select a leave type:");
            

            for (int i = 0; i < LeaveTypes.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {LeaveTypes[i]}");
            }
        }
    }
}
