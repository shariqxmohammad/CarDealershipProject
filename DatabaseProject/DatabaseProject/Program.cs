using System;
using System.Text.RegularExpressions;

namespace DatabaseProject
{
    class Program
    {
        private static MongoDataClient _dataClient = new MongoDataClient();

        static void Main(string[] args)
        {
            _dataClient.CustomersInTexasServedByFaith();
            WriteIntroductoryText();

            Console.WriteLine("Do you want to add the mock data?\n(y/n)");
            var mockDataInput = Console.ReadLine();

            var validMockDataInput = GetValidInput(mockDataInput);

            if (InputIsYes(validMockDataInput))
            {
                Console.WriteLine("Adding the data...");
                var dataFactory = new MockDataFactory(_dataClient);
                dataFactory.AddEverything();
            }
            else
            {
                Console.WriteLine("Not adding the data. Do you want to query your data?\n(y/n)");
                var queryInput = Console.ReadLine();
                var validQueryInput = GetValidInput(queryInput);
                if (InputIsYes(validQueryInput))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    var allVehicles = _dataClient.GetAllVehicles();
                    Console.WriteLine("\nGot all vehicles::");
                    foreach (var v in allVehicles)
                    {
                        Console.WriteLine("Vin: {0}\nYear: {1}\nCondition: {2}\nFuelType: {3}\n"
                            + "Transmission: {4}\nHorsepower: {5}\nColor: {6}\nPrice: {7}\nMiles: {8}\nModel: {9}\n",
                            v.Vin, v.Year, v.Condition, v.FuelType, v.Transmission, v.Horsepower,
                            v.Color, v.Price, v.Miles, v.Model);

                        Console.WriteLine("--------");
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;

                }
            }

            Console.ReadLine();
        }

        private static string GetValidInput(string input)
        {
            while (!IsValid(input))
            {
                Console.WriteLine("(y/n)");
                input = Console.ReadLine();
            }

            return input;
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

        private static bool InputIsYes(string input)
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
