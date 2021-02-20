using System;
using CustomerAPI.Entities;

namespace CustomerAPI.Services
{
    public interface ICustomerRepository
    {
        bool CustomerExists(Guid customerId);
        Customer GetCustomer(Guid customerId);
    }
}
