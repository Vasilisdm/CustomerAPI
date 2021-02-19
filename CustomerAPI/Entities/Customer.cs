using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAPI.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
