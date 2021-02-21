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
            CreateMap<Account, AccountDTO>();
            CreateMap<AccountDTO, Account>();
        }
    }
}
