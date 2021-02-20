using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public decimal Balance { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
