using System;
using System.Collections.Generic;

namespace CustomerAPI.Models
{
    public class AccountDTO
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public decimal Balance { get; set; }

        public List<TransactionDTO> Transactions { get; set; }
    }
}
