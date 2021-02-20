using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
