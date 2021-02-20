using System;
using CustomerAPI.Entities;

namespace CustomerAPI.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AccountContext _accountContext;

        public TransactionRepository(AccountContext accountContext)
        {
            _accountContext = accountContext ??
                throw new ArgumentNullException(nameof(accountContext));
        }

        public void MakeTransaction(Account account, decimal credit)
        {
            var accountTransaction = new Transaction()
            {
                AccountId = account.Id,
                Amount = credit
            };

            _accountContext.Transactions.Add(accountTransaction);
        }

        public bool SaveTransaction()
        {
            return (_accountContext.SaveChanges() >= 0);
        }
    }
}
