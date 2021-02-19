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

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int Balanace { get; set; }

        public List<int> Transactions { get; set; }
    }
}
