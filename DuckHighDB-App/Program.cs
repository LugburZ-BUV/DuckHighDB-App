using DuckHighDB_App.Models;
using DuckHighDB_App.UI;
using DuckHighDB_App.UI.MenuOptions;

namespace DuckHighDB_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleInputValidation consoleInputValidation = new ConsoleInputValidation();

            var dbContext = new DuckHighDbContext();

            IMenuOptions[] options = [
            new AddEmployee("Lägg till ny anställd", consoleInputValidation, dbContext),
            //new AllStudents("Se alla elever", consoleInputValidation, dbContext),
            //new AllStudentsByClass("Alla elever efter klass", consoleInputValidation, dbContext),
            ];

            MainMenu mainMenu = new MainMenu(options, consoleInputValidation);
            while (mainMenu.Show());
        }
    }
}
