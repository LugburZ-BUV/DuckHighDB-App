using DuckHighDB_App.UI.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI
{
    internal class MainMenu(IMenuOptions[] menuOptions, ConsoleInputValidation consoleInput)
    {
        /// <summary>
        /// Empty line, replaces \n.
        /// </summary>
        private readonly string _nl = Environment.NewLine;


        /// <summary>
        /// Displays the main menu for the user.
        /// </summary>
        public bool Show()
        {
            Console.WriteLine(CenterTitle("Huvudmeny"));

            Console.WriteLine($"{_nl}Välj ett av alternativen nedan:{_nl}");

            for (int index = 0; index < menuOptions.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {menuOptions[index].Title}");
            }

            Console.WriteLine($"{_nl}{menuOptions.Length + 1}. Avsluta{_nl}");
            int selectedMainMenu = consoleInput.ReadInteger("Ange ditt val: ", 1, menuOptions.Length + 1, "Ogiltigt val. Välj ett av alternativen i menyn.");

            if (selectedMainMenu == menuOptions.Length + 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{_nl}Programmet avslutas...{_nl}");
                Console.ResetColor();
                return false;
            }

            try
            {
                Console.Clear();
                menuOptions[selectedMainMenu - 1].Execute();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ett fel uppstod när alternativet skulle köras.");
                Console.ResetColor();
            }

            return true;
        }

        public string CenterTitle(string title)
        {
            int width = Console.WindowWidth;
            string nl = Environment.NewLine;
            string border = new string('=', width);

            if (title.Length >= width)
            {
                return border + nl + title + nl + border; // If the title is longer than the width, return it as is.
            }

            int leftSpacers = (width - title.Length) / 2;
            int rightSpacers = width - title.Length - leftSpacers;

            return border + nl + new string(' ', leftSpacers) + title + new string(' ', rightSpacers) + nl + border + nl;
        }
    }
}
