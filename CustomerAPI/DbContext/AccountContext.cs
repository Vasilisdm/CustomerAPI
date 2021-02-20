using System;
using CustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                    new Customer
                    {
                        Id  = Guid.Parse("d20b0d04-d371-48b7-99aa-0e2ac74ff98e"),
                        FirstName = "Vassilis",
                        LastName = "Dimitriou"
                    },
                    new Customer
                    {
                        Id = Guid.Parse("6eab5eb5-6131-45cf-809f-69d3e4120722"),
                        FirstName = "Kelly",
                        LastName = "Tokas"
                    }
                );
        }
    }
}
