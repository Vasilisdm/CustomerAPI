using System;
using CustomerAPI.Entities;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/openaccount")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccountController(IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            ICustomerRepository customerRepository)
        {
            _accountRepository = accountRepository ??
                throw new ArgumentNullException(nameof(accountRepository));

            _transactionRepository = transactionRepository ??
                throw new ArgumentNullException(nameof(transactionRepository));

            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));
        }


       [HttpPost("{initialCredit}")]
       public ActionResult OpenAccountForCustomer(Guid customerId, decimal initialCredit)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var newAccountId = _accountRepository.OpenAccount(customerId);
            _accountRepository.SaveAccount();

            if (initialCredit != 0)
            {
                var account = GetAccount(newAccountId);
                
                _transactionRepository.MakeTransaction(account, initialCredit);

                _transactionRepository.SaveTransaction();

                _accountRepository.ChangeBalance(account, initialCredit);

                _accountRepository.SaveAccount();

                //var customerFromRepo = _customerRepository.GetCustomer(customerId);
            }

            return Ok();
        }


        #region HelperMethods
        private Account GetAccount(Guid accountId)
        {
            if (accountId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            return _accountRepository.GetAccount(accountId);
        }
        #endregion
    }
}
