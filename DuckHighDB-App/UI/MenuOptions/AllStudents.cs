using DuckHighDB_App.Models;
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
            var students = dbContext.Students
                .OrderBy(s => s.StudentId)
                .ToList();

            //var enrollment = dbContext.Enrollment
            //    .OrderBy(e => e.StudentId)
            //    .ToList();


            //Console.WriteLine("Alla elever:");
            //foreach (var student in students)
            //{
            //    foreach (var Class in dbContext.Enro)
            //    {
            //        if (student.StudentId == Class.StudentId)
            //        {
            //            var classInfo = dbContext.Classes.FirstOrDefault(c => c.ClassId == studentClass.ClassId);
            //            if (classInfo != null)
            //            {
            //                student.Class = classInfo.Name;
            //            }
            //        }
            //    })
            //    Console.WriteLine($"ID: {student.StudentId}, Namn: {student.FirstName} {student.LastName}, Klass: {student.Class}");
            //}

            consoleInput.WaitForEnter();
            return;
        }
    }
}
