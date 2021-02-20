using System;
using System.Threading.Tasks;
using CustomerAPI.Entities;

namespace CustomerAPI.Services
{
    public interface IAccountRepository
    {
        public void OpenAccount(Guid customerId);
        public bool SaveAccount();
    }
}
