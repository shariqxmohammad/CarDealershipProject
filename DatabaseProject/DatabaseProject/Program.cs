using System;

namespace DatabaseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var _display = new DataDisplay();

            _display.WriteIntroductoryText();

            _display.InsertDataOption();

            _display.DisplayList();
            var input = Console.ReadLine();

            while (input != "q")
            {
                switch (input)
                {
                    case "1":
                        _display.DisplayAllVehicles();
                        break;
                    case "2":
                        _display.DisplayCountOfVehicles();
                        break;
                    case "3":
                        _display.DisplayAllTables();
                        break;
                    case "4":
                        _display.DisplayAllDealershipsInTexas();
                        break;
                    case "5":
                        _display.DisplayInnerJoinQuery();
                        break;
                    case "6":
                        _display.DisplayLeftJoinQuery();
                        break;
                    case "7":
                        _display.DisplayHavingAndGroupByQuery();
                        break;
                    case "8":
                        _display.DisplaySubQuery();
                        break;
                    default:
                        Console.WriteLine("That's not one of the options...");
                        break;
                }
                _display.DisplayList();
                input = Console.ReadLine();
            }

            Console.ReadLine();
        }
    }
}
