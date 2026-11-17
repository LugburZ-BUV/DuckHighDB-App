using DuckHighDB_App.UI;
using DuckHighDB_App.UI.MenuOptions;

namespace DuckHighDB_App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleInputValidation consoleInputValidation = new ConsoleInputValidation();

            IMenuOptions[] options = [
            new AddEmployee("Lägg till ny anställd", consoleInputValidation),
            new AllStudents("Se alla elever", consoleInputValidation),
            new AllStudentsByClass("Alla elever efter klass", consoleInputValidation),
            ];

            MainMenu mainMenu = new MainMenu(options, consoleInputValidation);
            while (mainMenu.Show());
        }
    }
}
