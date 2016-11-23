using System;
using System.Text.RegularExpressions;

namespace DatabaseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteIntroductoryText();

            Console.WriteLine("Do you want to add the mock data?\n(y/n)");
            var input = Console.ReadLine();

            while (!IsValid(input))
            {
                Console.WriteLine("(y/n)");
                input = Console.ReadLine();
            }

            if (ShouldAddMockData(input))
            {
                Console.WriteLine("Adding the data...");
                var dataFactory = new MockDataFactory();
                dataFactory.AddEverything();
            }
            else
                Console.WriteLine("Not adding the data.");

            Console.ReadLine();
        }

        private static bool IsValid(string input)
        {
            if (input.Length == 0) return false;

            var regex = "(^[yY]$|^[nN]$|^[yY][eE][sS]$|^[nN][oO]$)";

            if (Regex.IsMatch(input, regex))
                return true;
            else
                return false;
        }

        private static bool ShouldAddMockData(string input)
        {
            var addRegex = "(^[yY]$|^[yY][eE][sS]$)";
            var notAddRegex = "(^[nN]$|^[nN][oO]$)";

            if (Regex.IsMatch(input, addRegex))
                return true;
            else if (Regex.IsMatch(input, notAddRegex))
                return false;
            else
                throw new ArgumentException("Cannot determine whether to add data or not.");
        }

        private static void WriteIntroductoryText()
        {
            var title = "Dealership Database Project";
            var subtitle = "Shariq Mohammad\n";

            var intro = "This is the application that I made to add data into and query data from my database."
                + " I am using MongoDb as my database.This application first deserializes my data from json, then "
                + "stores it in MongoDb, allowing me to easily change the data that I have stored and query from the database.\n";

            Console.ForegroundColor = ConsoleColor.Green;
            CenterTextOutputInConsole(title);
            CenterTextOutputInConsole(subtitle);
            Console.WriteLine(intro);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void CenterTextOutputInConsole(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
