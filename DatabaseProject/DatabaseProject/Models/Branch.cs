using System;

namespace DatabaseProject.Models
{
    public class Branch
    {
        public string Phone { get; set; }
        public Address Address { get; set; }
        public Guid Id { get; set; }
    }
}
