using DuckHighDB_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{

    internal class AllStudentsByClass(string title, ConsoleInputValidation consoleInput, DuckHighDbContext dbContext) : IMenuOptions
    {
        public string Title => title;

        public void Execute()
        {

        }
    }




        //    StringBuilder BuildStudentListByClass(List<Student> students)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    var studentsByClass = students
        //        .SelectMany(s => s.Classes, (s, c) => new { Student = s, Class = c })
        //        .GroupBy(sc => sc.Class.Name)
        //        .OrderBy(g => g.Key);
        //    foreach (var classGroup in studentsByClass)
        //    {
        //        sb.AppendLine($"Klass: {classGroup.Key}");
        //        foreach (var student in classGroup)
        //        {
        //            sb.AppendLine($"  ID: {student.Student.StudentId}, Namn: {student.Student.FirstName} {student.Student.LastName}");
        //        }
        //    }
        //    return sb;
        //}
    }
