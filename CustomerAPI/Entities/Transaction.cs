using System;

namespace CustomerAPI.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public Guid AccountId { get; set; }

        public Account Account { get; set; }
    }
}
