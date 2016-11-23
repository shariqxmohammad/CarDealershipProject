using DatabaseProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseProject
{
    public class MongoDataClient
    {
        private const string _databaseName = "DealershipDatabase";
        private const string _mongoConnectionString = "mongodb://localhost:27017";
        private readonly IMongoDatabase _database;

        public MongoDataClient()
        {
            var client = new MongoClient(_mongoConnectionString);
            _database = client.GetDatabase(_databaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = typeof(T).Name;
            return _database.GetCollection<T>(collectionName);
        }

        public IEnumerable<Motorcycle> GetMotorcycles()
        {
            var collection = GetCollection<Motorcycle>();
            string sql = "";

            var result = collection.Find(sql).ToList();

            return result;
        }

        //Simple Query #1 - Select all vehicles from the Vehicles table.
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var collection = GetCollection<Vehicle>();

            var result = collection.Find(FilterDefinition<Vehicle>.Empty).ToList();

            var sqlResult = from v in result
                            select v;

            return sqlResult;
        }

        //Simple Query #2 - Select the number (count) of vehicles in the Vehicles table.
        public long GetNumberOfVehicles()
        {
            var collection = GetCollection<Vehicle>();

            return collection.Find(FilterDefinition<Vehicle>.Empty).Count();
        }

        //Simple Query #3 - Show all the tables in the database.
        public IEnumerable<string> ShowAllTables()
        {
            var allCollections = new List<string>();

            foreach(var collection in _database.ListCollections().ToList())
            {
                allCollections.Add(collection["name"].ToString());
            }

            return allCollections;
        }

        //Simple Query #4 - Query for branches only located in Texas.
        public IEnumerable<DealershipBranch> GetAllDealershipsInTexas()
        {
            var collection = GetCollection<DealershipBranch>();
            var filter = Builders<DealershipBranch>.Filter.Eq("Address.State", "TX");

            return collection.Find(filter).ToList();

        }


        //Complex Query #1 - Inner Join - Getting data from sales table and customer table: finding only customers
        //served by Faith Mandela
        public IEnumerable<Customer> GetAllCustomersServedByFaithMandela()
        {
            var faithMandelaId = new Guid("eec53a1f-2f33-4908-968c-33c93c351179");

            var salesCollection = GetCollection<Salesperson>();
            var customerCollection = GetCollection<Customer>();

            var allSalesPeople = salesCollection.Find(FilterDefinition<Salesperson>.Empty).ToList();
            var allCustomers = customerCollection.Find(FilterDefinition<Customer>.Empty).ToList();

            var customersServedByFaith = from c in allCustomers
                                         where c.ServedBy == faithMandelaId
                                         join s in allSalesPeople
                                         on c.ServedBy equals s.Id
                                         select new Customer
                                         {
                                             Address = c.Address,
                                             Name = c.Name,
                                             Ssn = c.Ssn,
                                             ServedBy = s.Id,
                                             SalesPerson = s.Name
                                         };

            return customersServedByFaith;
        }

        //Complex Query #2 - Left Join - Some customers do not yet have sales people because they are waiting in line. So when we left join
        //customers with sales people, we get all of the customers back, but we only set the customer.ServedBy and customer.SalesPerson values
        //if that customer has been served. This shit took 3 hours.
        public IEnumerable<Customer> LeftJoinSalesPeopleWhereSomeDoNotHaveCustomers()
        {
            var faithMandelaId = new Guid("eec53a1f-2f33-4908-968c-33c93c351179");

            var salesCollection = GetCollection<Salesperson>();
            var customerCollection = GetCollection<Customer>();

            var allSalesPeople = salesCollection.Find(FilterDefinition<Salesperson>.Empty).ToList();
            var allCustomers = customerCollection.Find(FilterDefinition<Customer>.Empty).ToList();

            var customers = from c in allCustomers
                            join s in allSalesPeople
                            on c.ServedBy equals s.Id
                            into gj
                            from sub in gj.DefaultIfEmpty()
                            select new Customer
                            {
                                Address = c.Address,
                                Name = c.Name,
                                Ssn = c.Ssn,
                                SalesPerson = (sub == null ? null : sub.Name),
                                ServedBy = (sub == null ? Guid.Empty : sub.Id)
                            };

            return customers;
        }

        //Complex Query #3 and #4 - Combining HAVING and GroupBy
        public void BranchesByStateAndCount()
        {
            var collection = GetCollection<DealershipBranch>();
            var aggregate = collection.Aggregate().Group(new BsonDocument { { "_id", "$Address.State" }, { "count", new BsonDocument("$sum", 1) } });
            var results = aggregate.ToList();
            foreach(var doc in results)
            {
                Console.WriteLine("State: {0} :: Count: {1}", doc["_id"], doc["count"]);
            }
        }

        //Complex Query #5 - Subquery - Select all customers that live in Texas from the selected list of all customers who were served by Faith Mandela.
        public void CustomersInTexasServedByFaith()
        {
            var faithMandelaId = new Guid("eec53a1f-2f33-4908-968c-33c93c351179");

            var salesCollection = GetCollection<Salesperson>();
            var customerCollection = GetCollection<Customer>();

            var allSalesPeople = salesCollection.Find(FilterDefinition<Salesperson>.Empty).ToList();
            var allCustomers = customerCollection.Find(FilterDefinition<Customer>.Empty).ToList();

            var customers = from c in (from z in allCustomers
                                       where z.ServedBy == faithMandelaId
                                       select z)
                            where c.Address.State == "TX"
                            select c;
           
        }
    }
}
