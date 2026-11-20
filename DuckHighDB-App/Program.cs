using DuckHighDB_App.Models;
using DuckHighDB_App.UI;
using DuckHighDB_App.UI.MenuOptions;

namespace DuckHighDB_App
{
    internal class Program
    {
        static void Main()
        {
            ConsoleInputValidation consoleInput = new ();

            DuckHighDbContext dbContext = new();

            StudentDisplayHelper studentDisplayHelper = new(consoleInput);

            IMenuOptions[] options = [
            new AddEmployee("Lägg till ny anställd", consoleInput, dbContext),
            new AllStudents("Se alla elever", consoleInput, dbContext, studentDisplayHelper),
            new AllStudentsByClass("Se alla elever per klass", consoleInput, dbContext, studentDisplayHelper),
            ];

            MainMenu mainMenu = new(options, consoleInput);

            while (mainMenu.Show());
        }
    }
}
