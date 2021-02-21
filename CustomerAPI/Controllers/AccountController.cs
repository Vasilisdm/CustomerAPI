using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CustomerAPI.Entities;
using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository ??
                throw new ArgumentNullException(nameof(accountRepository));

            _transactionRepository = transactionRepository ??
                throw new ArgumentNullException(nameof(transactionRepository));

            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("{accountId}", Name = "GetAccountForCustomer")]
        public ActionResult<AccountDTO> GetAccountForCustomer(Guid customerId, Guid accountId)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var accountForCustomer = _accountRepository.GetAccountForCustomer(customerId, accountId);
            if (accountForCustomer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AccountDTO>(accountForCustomer));
        }

        [HttpPost("{initialCredit}")]
        public ActionResult<AccountDTO> OpenAccountForCustomer(Guid customerId, decimal initialCredit)
        {
            if (!_customerRepository.CustomerExists(customerId))
            {
                return NotFound();
            }

            var newAccountId = _accountRepository.OpenAccount(customerId);
            _accountRepository.SaveAccount();

            var account = GetAccount(newAccountId);

            if (initialCredit != 0)
            {
                _transactionRepository.MakeTransaction(account, initialCredit);

                _transactionRepository.SaveTransaction();

                _accountRepository.ChangeBalance(account, initialCredit);

                _accountRepository.SaveAccount();
            }

            var accountToReturn = _mapper.Map<AccountDTO>(account);

            return CreatedAtRoute("GetAccountForCustomer", new {
                customerId,
                accountId = newAccountId
            }, accountToReturn);
        }

        [HttpGet()]
        public ActionResult<CustomerDTO> GetCustomerAccountInfo(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            var customerFromRepo = _customerRepository.GetCustomer(customerId);
            if (customerFromRepo == null)
            {
                return NotFound();
            }

            var customerAccounts = new List<AccountDTO>();
            foreach (var account in customerFromRepo.Accounts)
            {
                var accountDTO = _mapper.Map<AccountDTO>(account);
                customerAccounts.Add(accountDTO);
            }

            var customerToReturn = new CustomerDTO
            {
                FirstName = customerFromRepo.FirstName,
                LastName = customerFromRepo.LastName,
                Balance = customerFromRepo.Accounts.Sum(a => a.Balance),
                Accounts = customerAccounts
            };

            return Ok(customerToReturn);
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
