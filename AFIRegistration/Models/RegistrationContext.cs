﻿using Microsoft.EntityFrameworkCore;

namespace AFIRegistration.Models
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
