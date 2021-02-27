using System;

namespace AFIRegistration.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string PolicyNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Created { get; set; }
    }
}
