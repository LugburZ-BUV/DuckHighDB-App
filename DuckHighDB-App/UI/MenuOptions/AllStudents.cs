using DuckHighDB_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{

    /// <summary>
    /// MenuOption to show all students sorted by First/Last name,
    /// and choose direction, up/down.
    /// </summary>
    internal class AllStudents(string title, ConsoleInputValidation consoleInput, DuckHighDbContext dbContext) : IMenuOptions
    {

        private const int idSpace = -6;

        private const int firstNameSpace = -15;

        private const int lastNameSpace = -15;

        private const int ssnSpace = -17;

        private const int classSpace = -8;

        private const int emailSpace = -30;

        private const int lineSize = 100;


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


            DisplayStudents(students);
            Console.WriteLine();
            consoleInput.WaitForEnter();
            return;
        }

        /// <summary>
        /// Displays the specified students in a formatted table, including their ID,
        /// first name, last name, class, and email address.
        /// Students without an assigned class are displayed as "Ej tilldelad".
        /// </summary>
        /// <param name="students">The students to display.</param>
        void DisplayStudents(IEnumerable<Student> students)
        {

            Console.WriteLine($"{consoleInput.N1}{"ID",idSpace} {"Förnamn",firstNameSpace} {"Efternamn", lastNameSpace} {"Personnummer",ssnSpace} {"Klass",classSpace} {"E-post",emailSpace}");
            Console.WriteLine(new string('-', lineSize));

            foreach (var student in students)
            {
                var className = student.Classes.FirstOrDefault()?.Name ?? "Ej tilldelad";
                Console.WriteLine($"{student.StudentId,idSpace} {student.FirstName,firstNameSpace} {student.LastName,lastNameSpace} {student.Ssn,ssnSpace} {className,classSpace} {student.Email,emailSpace}");
            }
        }



        /// <summary>
        /// Stringbuilder for the sorting menu.
        /// </summary>
        /// <returns></returns>
       static StringBuilder BuildSortMenu()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Hur vill du sortera eleverna?");
            sb.AppendLine();
            sb.AppendLine("[1] > Sortera efter Förnamn");
            sb.AppendLine("[2] > Sortera efter Efternamn");
            sb.AppendLine();
            return sb;
        }


        /// <summary>
        /// Stringbuilder for the direction menu.
        /// </summary>
        /// <returns></returns>
       static StringBuilder BuildDirMenu()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Vilken sorteringsordning vill du använda?");
            sb.AppendLine();
            sb.AppendLine("[1] > Stigande: A-Ö");
            sb.AppendLine("[2] > Fallande: Ö-A");
            sb.AppendLine();
            return sb;
        }
    }
}
