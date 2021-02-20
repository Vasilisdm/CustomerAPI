using System;
using CustomerAPI.Entities;

namespace CustomerAPI.Services
{
    public interface ITransactionRepository
    {
        public void MakeTransaction(Account account, decimal credit);
        public bool SaveTransaction();
    }
}
