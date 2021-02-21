using System;
namespace CustomerAPI.Models
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }

        public Guid AccountId { get; set; }
    }
}
