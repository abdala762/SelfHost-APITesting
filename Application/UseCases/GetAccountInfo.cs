using Repository.Account;
using System;

namespace Application.UseCases
{
    public class GetAccountInfo : IGetAccountInfo
    {
        private readonly IAccountRepository _accountRepository;
        public GetAccountInfo(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountDto Execute(int accountNumber)
        {
            ValidateAccountNumber(accountNumber);

            var accountBalance = _accountRepository.GetAccountBalance(accountNumber);

            return new AccountDto
            {
                Balance = accountBalance
            };
        }

        private void ValidateAccountNumber(int accountNumber)
        {
            if (accountNumber.ToString().Length != 5)
            {
                throw new Exception("Account number must be a 5-digit number.");
            }
        }
    }
}
