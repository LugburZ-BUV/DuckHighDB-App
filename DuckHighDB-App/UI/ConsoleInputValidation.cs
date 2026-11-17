using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DuckHighDB_App.UI
{
    /// <summary>
    /// Validates and reads user input.
    /// </summary>
    public class ConsoleInputValidation
    {
        /// <summary>
        /// Default message to display when waiting for the user to press Enter.
        /// </summary>
        private const string DefaultWaitMessage = "Tryck på Enter för att återgå till huvudmenyn.";

        /// <summary> 
        /// Asks until the user enters an integer between min and max.
        /// </summary>
        /// <returns>A valid integer between min and max.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws only when min param is bigger than max param</exception>
        public int ReadInteger(string question, int min, int max, string errorMessage = null!)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

            errorMessage ??= $"Ogiltig inmatning. Ange ett tal mellan {min} och {max}.";
            Console.Write(question);

            int result;

            while (!int.TryParse(ReadLineOrThrow(), out result) || result < min || result > max)
            {
                Console.WriteLine(errorMessage);
                Console.Write(question);
            }

            return result;
        }

        /// <summary> 
        /// Asks until the user enters an amount between min and max with at most two decimals.
        /// </summary>
        /// <returns>A valid amount between min and max.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws only when min param is bigger than max param</exception>
        public decimal ReadDecimal(string question, decimal max, decimal min = 0.01m)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(min, max);

            Console.Write(question);

            decimal result;

            while (!IsValidDecimal(ReadLineOrThrow(), min, max, out result))
            {
                Console.WriteLine($"Ogiltigt belopp. Ange ett belopp mellan {min:C} och {max:C}, med max två decimaler.");
                Console.Write(question);
            }

            return result;
        }

        /// <summary>
        /// Checks that the input is a number between min and max with at most two decimals.
        /// And "." is accepted as well as "," so both 238,50 and 238.50 will work.
        /// </summary>
        /// <returns>True if the input is a valid amount, otherwise false.</returns>
        private static bool IsValidDecimal(string input, decimal min, decimal max, out decimal amount)
        {
            bool isNumber = decimal.TryParse(input.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out amount);
            bool isInRange = amount >= min && amount <= max;
            bool hasAtMostTwoDecimals = decimal.Round(amount, 2) == amount;

            return isNumber && isInRange && hasAtMostTwoDecimals;
        }

        /// <summary>
        /// Asks until the user enters a non-empty text. Surrounding spaces are removed.
        /// Limits the length of the text to maxLength.
        /// </summary>
        /// <returns>String after validation; otherwise infinity loop until correct input</returns>
        public string ReadText(string question, int maxLength = 50)
        {
            Console.Write(question);
            string result = ReadLineOrThrow();

            while (string.IsNullOrWhiteSpace(result) || result.Length > maxLength)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fältet får inte vara tomt eller överstiga {maxLength} tecken.");
                Console.ResetColor();
                Console.Write(question);
                result = ReadLineOrThrow();
            }

            return result.Trim();
        }

        /// <summary>
        /// Asks until the user enters one of the two allowed characters (case-insensitive).
        /// </summary>
        /// <returns>The chosen character in uppercase.</returns>
        public char ReadChar(string question, char char1 = 'j', char char2 = 'n')
        {
            char upper1 = char.ToUpperInvariant(char1);
            char upper2 = char.ToUpperInvariant(char2);

            Console.Write(question);
            string input = ReadLineOrThrow().Trim().ToUpperInvariant();

            while (input.Length != 1 || (input[0] != upper1 && input[0] != upper2))
            {
                Console.WriteLine($"Ogiltigt val. Ange '{upper1}' eller '{upper2}'.");
                Console.Write(question);
                input = ReadLineOrThrow().Trim().ToUpperInvariant();
            }

            return input[0];
        }

        /// <summary>
        /// Waits for the user to press Enter before continuing.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void WaitForEnter(string message = DefaultWaitMessage)
        {
            Console.WriteLine(message);
            ReadLineOrThrow();
        }

        /// <summary> 
        /// Reads a line from the console.
        /// </summary>
        /// <returns>The entered line.</returns>
        /// <exception cref="EndOfStreamException">Thrown when input is closed, to avoid null input</exception>
        private static string ReadLineOrThrow()
        {
            return Console.ReadLine() ?? throw new EndOfStreamException("Inmatningen avslutades oväntat.");
        }

        /// <summary>
        /// Asks the user for a valid UserID from the list.
        /// </summary>
        /// <returns></returns>
        public int ReadValidId(string question, List<int> validIds)
        {
            Console.Write(question);
            int result;

            while (!int.TryParse(ReadLineOrThrow(), out result) || !validIds.Contains(result))
            {
                Console.WriteLine("Ogiltigt ID. Välj ett ID från listan ovan.");
                Console.Write(question);
            }

            return result;
        }

        /// <summary>
        /// Validates the user input and checks that the SSN is formatted properly.
        /// </summary>
        /// <returns></returns>
        public string ReadSsn(string question)
        {
            Console.Write(question);
            string input = ReadLineOrThrow().Trim();

            while (!Regex.IsMatch(input, @"^\d{8}-\d{4}$"))
            {
                Console.WriteLine("Ogiltigt personnummer. Ange formatet ÅÅÅÅMMDD-XXXX (13 tecken).");
                Console.Write(question);
                input = ReadLineOrThrow().Trim();
            }

            return input;
        }

        public readonly string N1 = Environment.NewLine;
    }
}
