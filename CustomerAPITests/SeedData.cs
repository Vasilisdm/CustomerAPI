using System;
using CustomerAPI;
using CustomerAPI.Entities;

namespace CustomerAPITests
{
    public class SeedData
    {
        public static void PopulateTestData(AccountContext accountContext)
        {
            accountContext.Customers.Add(new Customer
            {
                Id = Guid.Parse("d20b0d04-d371-48b7-99aa-0e2ac74ff98e"),
                FirstName = "Vassilis",
                LastName = "Dimitriou"
            });
        }        
    }
}
