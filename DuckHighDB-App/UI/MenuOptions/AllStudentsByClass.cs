using DuckHighDB_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{
    /// <summary>
    /// Represents a menu option that displays all students belonging to a selected class.
    /// Classes are presented in alphabetical order and include their corresponding school year.
    /// </summary>
    internal class AllStudentsByClass(string title, ConsoleInputValidation consoleInput, DuckHighDbContext dbContext, StudentDisplayHelper studentHelper) : IMenuOptions
    {
        public string Title => title;

        /// <summary>
        /// Displays the available classes, prompts the user to select one,
        /// and displays all students assigned to the selected class.
        /// </summary>
        public void Execute()
        {
            var classes = dbContext.Classes
                .OrderBy(c => c.Name)
                .ToList();

            Console.WriteLine($"Välj en klass för att se eleverna:{consoleInput.N1}");
            for (int i = 0; i < classes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] > {classes[i].Name} ({classes[i].SchoolYear})");
            }
            Console.WriteLine();

            int choice = consoleInput.ReadInteger("Ange ditt val: ", 1, classes.Count, "Ogiltigt val.");
            var selectedClass = classes[choice - 1];

            var studentsInClass = dbContext.Students
            .Include(s => s.Classes)
            .Where(s => s.Classes.Any(c => c.ClassId == selectedClass.ClassId));

            studentHelper.SortAndDisplay(studentsInClass);
        }
    }
}
