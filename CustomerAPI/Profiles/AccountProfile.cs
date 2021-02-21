using System;
using AutoMapper;
using CustomerAPI.Entities;
using CustomerAPI.Models;

namespace CustomerAPI.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Transaction, TransactionDTO>();

            CreateMap<Account, AccountDTO>();
        }
    }
}
