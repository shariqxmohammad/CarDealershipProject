using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DatabaseProject.Models
{
    public class Customer
    { 
        [BsonId]
        public string Ssn { get; set; }

        public Name Name { get; set; }
        public Address Address { get; set; }
        public Guid ServedBy { get; set; }
        public Name SalesPerson { get; set; }
    }
}
