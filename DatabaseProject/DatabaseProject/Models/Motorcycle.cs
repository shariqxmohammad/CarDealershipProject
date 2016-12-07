using System;

namespace DatabaseProject.Models
{
    public class Motorcycle
    {
        public BikeType Type { get; set; }
    }

    public enum BikeType
    {
        Conventional,
        Sport,
        Dirt, 
        Scooter
    }
}
