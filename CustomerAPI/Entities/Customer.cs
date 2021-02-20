using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Account> Account { get; set; } = new List<Account>();
    }
}
