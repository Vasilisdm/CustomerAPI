using System;
using CustomerAPI.Entities;

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

        public void OpenAccount(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            var newAccountForCustomer = new Account();
            newAccountForCustomer.Id = Guid.NewGuid();
            newAccountForCustomer.CustomerId = customerId;

            _accountContext.Accounts.Add(newAccountForCustomer);
        }

        public bool SaveAccount()
        {
            return (_accountContext.SaveChanges() >= 0);
        }
    }
}
