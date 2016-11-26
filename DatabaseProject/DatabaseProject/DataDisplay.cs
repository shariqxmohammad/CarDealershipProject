using DatabaseProject.Models;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabaseProject
{
    public class DataDisplay
    {
        private readonly MongoDataClient _dataClient;

        public DataDisplay()
        {
            _dataClient = new MongoDataClient();
        }

        public void DisplayList()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            var intro = "Select one of the following options to execute a query:\n\n";
            var first = "1. Select all vehicles from vehicles table.\n";
            var second = "2. Get the COUNT of vehicles in the vehicles table.\n";
            var third = "3. Retrieve and display all tables in the database\n";
            var fourth = "4. Query for dealership branches that are only located in Texas\n";
            var fifth = "5. Inner join: find and display data from both the sales and customer tables only served by Faith Mandela\n";
            var sixth = "6. Left join: get all customers, but for those that have been served also display their server information.\n";
            var seventh = "7. Having and Groupby states\n";
            var eigth = "8. Subquery: select all customers that live in Texas from the selected list of all customers who were served by Faith Mandela\n";

            var stringBuilder = new StringBuilder(intro + first + second + third + fourth + fifth + sixth + seventh + eigth);

            Console.WriteLine(stringBuilder);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayAllVehicles()
        {
            var allVehicles = _dataClient.GetAllVehicles();

            foreach(var v in allVehicles)
            {
                Console.WriteLine($"Vin: {v.Vin} \nYear: {v.Year} \nCondition: {v.Condition} \nFuelType: {v.FuelType} \nTransmission: {v.Transmission} \nHorsepower: {v.Horsepower} \nColor: {v.Color} \nPrice: {v.Price} \nMiles: {v.Miles} \nModel: {v.Model}\n");
            }
        }

        public void DisplayCountOfVehicles()
        {
            Console.WriteLine(_dataClient.GetNumberOfVehicles());
        }

        public void DisplayAllTables()
        {
            var tables = _dataClient.ShowAllTables();

            foreach(var table in tables)
            {
                Console.WriteLine(table);
            }
        }

        public void DisplayAllDealershipsInTexas()
        {
            var dealerships = _dataClient.GetAllDealershipsInTexas();

            foreach (var d in dealerships)
            {
                Console.WriteLine($"Id: {d.Id} \nAddress:" + DisplayAddress(d.Address) + $" \nPhone: {d.Phone}\n");
            }
        }

        public void DisplayInnerJoinQuery()
        {
            var customers = _dataClient.GetAllCustomersServedByFaithMandela();

            foreach (var c in customers)
            {
                Console.WriteLine($"Ssn: {c.Ssn} \nName:" + DisplayName(c.Name) + "\nAddress:" + DisplayAddress(c.Address) + $"\nServedBy: {c.ServedBy} \nSalesPerson: " + DisplayName(c.SalesPerson) + "\n");
            }
        }

        public void DisplayLeftJoinQuery()
        {
            var customers = _dataClient.LeftJoinSalesPeopleWhereSomeDoNotHaveCustomers();

            foreach (var c in customers)
            {
                Console.WriteLine($"Ssn: {c.Ssn} \nName:" + DisplayName(c.Name) + "\nAddress:" + DisplayAddress(c.Address) + $"\nServedBy: {c.ServedBy} \nSalesPerson: " + DisplayName(c.SalesPerson) + "\n");
            }
        }

        public void DisplayHavingAndGroupByQuery()
        {
            _dataClient.BranchesByStateAndCount();
        }

        public void DisplaySubQuery()
        {
            var customers = _dataClient.CustomersInTexasServedByFaith();

            foreach (var c in customers)
            {
                Console.WriteLine($"Ssn: {c.Ssn} \nName:" + DisplayName(c.Name) + "Address:" + DisplayAddress(c.Address) + $"\nServedBy: {c.ServedBy} \nSalesPerson: " + DisplayName(c.SalesPerson) +"\n");
            }
        }

        public void InsertDataOption()
        {
            Console.WriteLine("Do you want to add the mock data?\n(y/n)");
            var mockDataInput = Console.ReadLine();

            var validMockDataInput = GetValidInput(mockDataInput);

            if (InputIsYes(validMockDataInput))
            {
                Console.WriteLine("Adding the data...");
                var dataFactory = new MockDataFactory(_dataClient);
                dataFactory.AddEverything();
            }

            Console.WriteLine();
        }

        public void WriteIntroductoryText()
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

        #region HelperMethods
        private string DisplayAddress(Address address)
        {
            return address.Street + ", " + address.City + ", " + address.State;
        }

        private string DisplayName(Name name)
        {
            if (name == null) return "none";
            return name.First + " " + name.Last;
        }

        private string GetValidInput(string input)
        {
            while (!IsValid(input))
            {
                Console.WriteLine("(y/n)");
                input = Console.ReadLine();
            }

            return input;
        }

        private bool IsValid(string input)
        {
            if (input.Length == 0) return false;

            var regex = "(^[yY]$|^[nN]$|^[yY][eE][sS]$|^[nN][oO]$)";

            if (Regex.IsMatch(input, regex))
                return true;
            else
                return false;
        }

        private bool InputIsYes(string input)
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

        private void CenterTextOutputInConsole(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }
        #endregion
    }
}
