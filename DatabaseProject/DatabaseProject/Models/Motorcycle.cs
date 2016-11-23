namespace DatabaseProject.Models
{
    public class Motorcycle : Vehicle
    {
        public BikeType Type { get; set; }
    }

    public enum BikeType
    {
        LowRider,
        HighRider,
        Bicycle,
        BranchWarrensHorse
    }
}
