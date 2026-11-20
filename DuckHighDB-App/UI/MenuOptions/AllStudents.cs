using DuckHighDB_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{

    /// <summary>
    /// MenuOption to show all students, user chooses to sort by First/Last name,
    /// and which direction, up/down.
    /// </summary>
    internal class AllStudents(string title, DuckHighDbContext dbContext, StudentDisplayHelper studentHelper) : IMenuOptions
    {
        public string Title => title;

        public void Execute()
        {

            var students = dbContext.Students.Include(s => s.Classes).AsQueryable();

            studentHelper.SortAndDisplay(students);
        }
    }
}
