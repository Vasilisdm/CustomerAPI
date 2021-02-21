using System.Collections.Generic;

namespace CustomerAPI.Models
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Balance { get; set; }

        public List<AccountDTO> Accounts { get; set; }
    }
}
