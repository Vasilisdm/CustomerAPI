using System;
using System.Linq;
using CustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _accountContext;

        public AccountRepository(AccountContext accountContext)
        {
            _accountContext = accountContext ??
                throw new ArgumentNullException(nameof(accountContext));
        }

        public Guid OpenAccount(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            var newAccountForCustomer = new Account
            {
                CustomerId = customerId
            };

            _accountContext.Accounts.Add(newAccountForCustomer);

            return newAccountForCustomer.Id;
        }

        public Account GetAccount(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            return _accountContext.Accounts.FirstOrDefault(account => account.Id == accountId);
        }

        public void ChangeBalance(Account account, decimal credit)
        {
            account.Balance += credit;
            _accountContext.Entry(account).State = EntityState.Modified;
        }

        public bool SaveAccount()
        {
            return (_accountContext.SaveChanges() >= 0);
        }
    }
}
