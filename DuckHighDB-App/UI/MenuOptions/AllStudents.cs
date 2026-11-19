using DuckHighDB_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{
    internal class AllStudents(string title, ConsoleInputValidation consoleInput, DuckHighDbContext dbContext) : IMenuOptions
    {
        public string Title => title;

        public void Execute()
        {

            Console.Write(BuildSortMenu());
            var sortChoice = consoleInput.ReadInteger("Ange ditt val: ", 1, 2, "Ogiltigt val. Välj 1 eller 2.");


            Console.Write(BuildDirMenu());
            var dirChoice = consoleInput.ReadInteger("Ange ditt val: ", 1, 2, "Ogiltigt val. Välj 1 eller 2.");

            var students = dbContext.Students.Include(s => s.Classes).AsQueryable();

            if (sortChoice == 1)
            {
                students = dirChoice == 1 ? students.OrderBy(s => s.FirstName) : students.OrderByDescending(s => s.FirstName);
            }
            else
            {
                students = dirChoice == 1 ? students.OrderBy(s => s.LastName) : students.OrderByDescending(s => s.LastName);
            }

            DisplayStudents(students.ToList());
            Console.WriteLine();
            consoleInput.WaitForEnter();
            return;
        }

        void DisplayStudents(List<Student> students)
        {

            Console.WriteLine($"{consoleInput.N1}{"ID",-6} {"Förnamn",-15} {"Efternamn",-15} {"Klass",-8} {"E-post",-30}");
            Console.WriteLine(new string('-', 78));

            foreach (var student in students)
            {
                var className = student.Classes.FirstOrDefault()?.Name ?? "Ej tilldelad";
                Console.WriteLine($"{student.StudentId,-6} {student.FirstName,-15} {student.LastName,-15} {className,-8} {student.Email,-30}");
            }
        }



        StringBuilder BuildSortMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Hur vill du sortera eleverna?");
            sb.AppendLine();
            sb.AppendLine("[1] > Sortera efter Förnamn");
            sb.AppendLine("[2] > Sortera efter Efternamn");
            sb.AppendLine();
            return sb;
        }

        StringBuilder BuildDirMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Vilken sorteringsordning vill du använda?");
            sb.AppendLine();
            sb.AppendLine("[1] > Stigande: A-Ö");
            sb.AppendLine("[2] > Fallande: Ö-A");
            sb.AppendLine();
            return sb;
        }
    }
}
