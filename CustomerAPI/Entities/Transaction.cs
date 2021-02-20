using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public int Transact { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public Guid AccountId { get; set; }
    }
}
