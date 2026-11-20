using DuckHighDB_App.UI.MenuOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI
{
    internal class MainMenu(IMenuOptions[] menuOptions, ConsoleInputValidation consoleInput)
    {

        /// <summary>
        /// Displays the main menu.
        /// </summary>
        public bool Show()
        {
            Console.Clear();
            Console.WriteLine(CenterTitle("Huvudmeny"));

            Console.WriteLine($"{consoleInput.N1}Välj ett av alternativen nedan:{consoleInput.N1}");

            for (int index = 0; index < menuOptions.Length; index++)
            {
                Console.WriteLine($"[{index + 1}] > {menuOptions[index].Title}");
            }

            Console.WriteLine($"{consoleInput.N1}[{menuOptions.Length + 1}] > Avsluta{consoleInput.N1}");
            int selectedMainMenu = consoleInput.ReadInteger("Ange ditt val: ", 1, menuOptions.Length + 1, "Ogiltigt val. Välj ett av alternativen i menyn.");

            if (selectedMainMenu == menuOptions.Length + 1)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{consoleInput.N1}Programmet avslutas...{consoleInput.N1}");
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

        /// <summary>
        /// Centers the provided string and adds a border across the width of the screen below and above.
        /// </summary>
        /// <returns>The border + Title as a string.</returns>
        public string CenterTitle(string title)
        {
            int width = Console.WindowWidth;
            string nl = consoleInput.N1;
            string border = new('=', width);

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
