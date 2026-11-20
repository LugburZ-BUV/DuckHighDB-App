using DuckHighDB_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{
    internal class AddEmployee(string title, ConsoleInputValidation consoleInput, DuckHighDbContext dbContext) : IMenuOptions
    {
        public string Title => title;

        public void Execute()
        {
            var positions = dbContext.EmployeePositions
            .OrderBy(p => p.PositionId)
            .ToList();

            var Departments = dbContext.Departments
            .OrderBy(d => d.DepartmentId)
            .ToList();


            string firstName = consoleInput.ReadText("Ange förnamn: ", 50);

            string lastName = consoleInput.ReadText("Ange efternamn: ", 50);

            string ssn = consoleInput.ReadSsn("Ange personnummer: ");

            decimal salary = consoleInput.ReadDecimal("Ange lön: ", 10000000000m, 0m);

            Console.WriteLine("Tillgängliga befattningar:");
            foreach (var p in positions)
            {
                Console.WriteLine($"{p.PositionId}. {p.Name}");
            }
            int position = consoleInput.ReadInteger("Ange befattning: ", 1, 
                positions.Count, 
                "Ogiltigt val. Ange ett giltigt ID för befattningen.");

            Console.WriteLine("Tillgängliga avdelningar:");
            foreach (var d in Departments)
            {
                Console.WriteLine($"{d.DepartmentId}. {d.Name}");
            }
            int department = consoleInput.ReadInteger("Ange avdelning: ", 1, 
                Departments.Count, 
                "Ogiltigt val. Ange ett giltigt ID för avdelningen.");

            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Ssn = ssn,
                HireDate = DateOnly.FromDateTime(DateTime.Now),
                Salary = salary,
                PositionId = position,
                DepartmentId = department
            };


            Console.WriteLine($"{consoleInput.N1}Berkräfta ny anställd: ");
            PrintEmployeeSummary(newEmployee, dbContext);


            Console.WriteLine();
            char choice = consoleInput.ReadChar("Lägg till anställd? (Y/N): ");
            if (choice == 'Y' || choice == 'y')
            {
                dbContext.Employees.Add(newEmployee);
                dbContext.SaveChanges();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ny anställd tillagd.");
                Console.ResetColor();
                consoleInput.WaitForEnter();
                return;
            }
            else
                {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Åtgärd avrbuten, ingen anställd lades till.");
                Console.ResetColor();
                consoleInput.WaitForEnter();
                return;
            }


        }

        /// <summary>
        /// Prints the id, name, ssn, salary, position, department
        /// of the employee.
        /// </summary>
        private static void PrintEmployeeSummary(Employee employee, DuckHighDbContext dbContext)
        {
            if (employee.EmployeeId > 0)
            {
                Console.WriteLine($"ID:             {employee.EmployeeId}");
            }

            Console.WriteLine($"Namn:           {employee.FirstName} {employee.LastName}");
            Console.WriteLine($"Personnummer:   {employee.Ssn}");
            Console.WriteLine($"Lön:            {employee.Salary:C}");
            Console.WriteLine($"Befattning:     {dbContext.EmployeePositions.Find(employee.PositionId)?.Name}");
            Console.WriteLine($"Avdelning:      {dbContext.Departments.Find(employee.DepartmentId)?.Name}");
        }
    }
}
