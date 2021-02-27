using System;

namespace AFIRegistration.Models
{
    public class Customer : CustomerDTO
    {
        public int CustomerId { get; set; }

        public DateTime Created { get; set; }
    }
}
