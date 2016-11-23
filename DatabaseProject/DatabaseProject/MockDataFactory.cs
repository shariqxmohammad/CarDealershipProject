using DatabaseProject.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DatabaseProject
{
    public class MockDataFactory
    {
        private readonly IMongoDatabase _database;

        public MockDataFactory()
        {
            _database = MongoDataClient.GetDealershipDatabase();
        }

        public void AddVehicles()
        {
            var collection = GetCollection<Vehicle>();
            var vehicles = Deserialize<Vehicle>("vehicles.json");

            collection.InsertMany(vehicles);
        }

        public void AddMotorcycles()
        {
            var collection = GetCollection<Motorcycle>();
            var motorcycles = Deserialize<Motorcycle>("motorcycles.json");

            collection.InsertMany(motorcycles);
        }

        public void AddCars()
        {
            var collection = GetCollection<Car>();
            var cars = Deserialize<Car>("cars.json");

            collection.InsertMany(cars);
        }

        public void AddSuvs()
        {
            var collection = GetCollection<Suv>();
            var suvs = Deserialize<Suv>("suvs.json");

            collection.InsertMany(suvs);
        }

        public void AddTrucks()
        {
            var collection = GetCollection<Truck>();
            var trucks = Deserialize<Truck>("trucks.json");

            collection.InsertMany(trucks);
        }

        public void AddCustomers()
        {
            var collection = GetCollection<Truck>();
            var customers = Deserialize<Truck>("customers.json");

            collection.InsertMany(customers);
        }

        public void AddSalesPeople()
        {
            var collection = GetCollection<Truck>();
            var salespeople = Deserialize<Truck>("salespeople.json");

            collection.InsertMany(salespeople);
        }

        public void AddDealerships()
        {
            var collection = GetCollection<Truck>();
            var dealerships = Deserialize<Truck>("branches.json");

            collection.InsertMany(dealerships);
        }

        public void AddEverything()
        {
            AddVehicles();
            AddMotorcycles();
            AddCars();
            AddSuvs();
            AddTrucks();

            AddCustomers();
            AddSalesPeople();
            AddDealerships();
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = typeof(T).Name;
            return _database.GetCollection<T>(collectionName);
        }

        private IEnumerable<T> Deserialize<T>(string jsonFileName)
        {
           return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonFileName);
        }
    }
}
