using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DatabaseProject.Models
{
    public class Vehicle
    {
        [BsonId]
        public string Vin { get; set; }

        public string Year { get; set; }
        public string Condition { get; set; }
        public FuelType FuelType { get; set; }
        public Transmission Transmission { get; set; }
        public string Horsepower { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string Miles { get; set; }
        public string Model { get; set; }
    }

    public enum FuelType
    {
        Unleaded,
        Premium
    }

    public enum Transmission
    {
        Stick,
        Automatic
    }
}
