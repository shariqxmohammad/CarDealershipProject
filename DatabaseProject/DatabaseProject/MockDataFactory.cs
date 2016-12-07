﻿using DatabaseProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseProject
{
    public class MockDataFactory
    {
        private readonly MongoDataClient _dataClient;

        public MockDataFactory(MongoDataClient dataClient)
        {
            if (dataClient == null) throw new ArgumentNullException(nameof(dataClient));

            _dataClient = dataClient;
        }

        public void AddVehicles()
        {
            var collection = _dataClient.GetCollection<Vehicle>();
            var vehicles = Deserialize<Vehicle>("vehicles.json");

            collection.InsertMany(vehicles);
        }

        public void AddMotorcycles()
        {
            var collection = _dataClient.GetCollection<Motorcycle>();
            var motorcycles = Deserialize<Motorcycle>("motorcycles.json");

            collection.InsertMany(motorcycles);
        }

        public void AddCars()
        {
            var collection = _dataClient.GetCollection<Car>();
            var cars = Deserialize<Car>("cars.json");

            collection.InsertMany(cars);
        }

        public void AddSuvs()
        {
            var collection = _dataClient.GetCollection<Suv>();
            var suvs = Deserialize<Suv>("suvs.json");

            collection.InsertMany(suvs);
        }

        public void AddTrucks()
        {
            var collection = _dataClient.GetCollection<Truck>();
            var trucks = Deserialize<Truck>("trucks.json");

            collection.InsertMany(trucks);
        }

        public void AddCustomers()
        {
            var collection = _dataClient.GetCollection<Customer>();
            var customers = Deserialize<Customer>("customers.json");

            collection.InsertMany(customers);
        }

        public void AddSalesPeople()
        {
            var collection = _dataClient.GetCollection<Salesperson>();
            var salespeople = Deserialize<Salesperson>("salespeople.json");

            collection.InsertMany(salespeople);
        }

        public void AddDealerships()
        {
            var collection = _dataClient.GetCollection<DealershipBranch>();
            var dealerships = Deserialize<DealershipBranch>("branches.json");

            collection.InsertMany(dealerships);
        }

        public void AddEverything()
        {
            try
            {
                AddVehicles();
                AddMotorcycles();
                AddCars();
                AddSuvs();
                AddTrucks();

                AddCustomers();
                AddSalesPeople();
                AddDealerships();

                Console.WriteLine("Successfully added all the mock data.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to add mock data:: {0}", e);
            }
        }

        private IEnumerable<T> Deserialize<T>(string jsonFileName)
        {
            var dataDirectory = "./Data/";
            var filePath = dataDirectory + jsonFileName;

            IEnumerable<T> rvalue = null;

            try
            {
                rvalue = JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                Console.WriteLine("Fail to deserialize object of type {0}.", nameof(T));
            }

            return rvalue;
        }
    }
}
