using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CustomerAPI.Models;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ??
                throw new ArgumentNullException(nameof(customerRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
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
    }
}
