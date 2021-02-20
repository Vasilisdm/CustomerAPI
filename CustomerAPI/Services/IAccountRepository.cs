using System;
using System.Threading.Tasks;
using CustomerAPI.Entities;

namespace CustomerAPI.Services
{
    public interface IAccountRepository
    {
        public Guid OpenAccount(Guid customerId);
        public Account GetAccount(Guid accountId);
        public void ChangeBalance(Account account, decimal credit);
        public bool SaveAccount();
    }
}
