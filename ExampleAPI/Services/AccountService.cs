namespace ExampleAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;

        private static Dictionary<string, Account> _accounts = new Dictionary<string, Account>();

        public AccountService(ILogger<AccountService> logger)
        {
            _logger = logger;         
        }


        public Dictionary<string, Account> All()
        {
            return _accounts;
        }

        public void DeleteAll()
        {
            _accounts = new Dictionary<string, Account>();
        }


        public AccountResponse Create(string userId, decimal amount)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return new AccountResponse(false, Constants._userRequiredToCreateAccount);
            }

            if(amount < Constants._minimumAmount)
            {
                return new AccountResponse(false, Constants._minimumAmountMessage);
            }

            var newAccount = new Account(userId, amount);
            _accounts.Add(newAccount.Id, newAccount);
            return new AccountResponse(true, Constants._accountCreateSuccess, newAccount.Id);

        }

        public AccountResponse Delete(string userId, string accountId)
        {
            //check if the account exists
            if (_accounts.ContainsKey(accountId))
            {
                var account = _accounts[accountId];
                //check if the account belongs to the user
                if (account.UserId == userId)
                {
                    //remove the account
                    _accounts.Remove(accountId);
                    return new AccountResponse(true, Constants._accountDeleteSuccess, accountId);
                }

                return new AccountResponse(false, Constants._accountDeleteFailureUserMismatch, accountId);
            }

            return new AccountResponse(false, Constants._accountDoesNotExist, accountId);
        }

        public AccountResponse Withdraw(string userId, string accountId, decimal amount)
        {
            //check if the account exists
            if (_accounts.ContainsKey(accountId))
            {
                var account = _accounts[accountId];
                //check if the account belongs to the user
                if (account.UserId == userId)
                {
                    //ensure they are not withdrawing more than 90% of balance
                    var preBalance = account.Balance;
                    if (amount > Constants._maxPerTransactionWithdrawalPercent * preBalance / 100)
                    {
                        return new AccountResponse(false, Constants._maxPerTransactionWithdrawalPercentMessage, accountId);
                    }

                    //calculate the final balance in the account
                    var postBalance = preBalance - amount;
                    if (postBalance < Constants._minimumAmount)
                    {
                        return new AccountResponse(false, Constants._minimumAmountMessage, accountId);
                    }
                    else
                    {
                        //subtract from the balance
                        account.Balance = postBalance;
                        return new AccountResponse(true, Constants._accountWithdrawalSuccess + " " + amount, accountId);
                    }
                }

                return new AccountResponse(false, Constants._accountWithdrawalFailureUserMismatch, accountId);
            }

            return new AccountResponse(false, Constants._accountDoesNotExist, accountId);
        }

        public AccountResponse Deposit(string accountId, decimal amount)
        {
           
            //check if the account exists
            if (_accounts.ContainsKey(accountId))
            {
                var account = _accounts[accountId];

                //calculate the final balance in the account
                var preBalance = account.Balance;
                var postBalance = account.Balance + amount;
                if (amount > Constants._maxDepositAmount)
                {
                    return new AccountResponse(false, Constants._maxDepositAmountMessage, accountId);
                }
                else
                {
                    //add to from the balance
                    account.Balance = postBalance;
                    return new AccountResponse(true, Constants._accountDepositSuccess + " " + amount, accountId);
                }
            }

            return new AccountResponse(false, Constants._accountDoesNotExist, accountId);
        }
    }
}
