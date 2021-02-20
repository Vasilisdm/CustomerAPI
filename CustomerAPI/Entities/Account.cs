using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public int Balanace { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}
