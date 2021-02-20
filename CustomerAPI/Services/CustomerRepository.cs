using System;
using System.Linq;
using CustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AccountContext _accountContext;

        public CustomerRepository(AccountContext accountContext)
        {
            _accountContext = accountContext ??
                throw new ArgumentNullException(nameof(accountContext));
        }

        public bool CustomerExists(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            return _accountContext.Customers.Any(customer => customer.Id == customerId);
        }

        public Customer GetCustomer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            return _accountContext.Customers
                .Include(customer => customer.Account)
                .FirstOrDefault(customer => customer.Id == customerId);
        }
    }
}
