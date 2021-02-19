using System;
using System.Collections.Generic;
using CustomerAPI.Entities;

namespace CustomerAPI.Models
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Balance { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
