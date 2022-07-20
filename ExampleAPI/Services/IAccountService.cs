
namespace ExampleAPI.Services
{
    public interface IAccountService
    {
        Dictionary<string, Account> All();

        void DeleteAll();
        AccountResponse Create(string userId, decimal amount);
        AccountResponse Delete(string userId, string accountId);
        AccountResponse Deposit(string accountId, decimal amount);
        AccountResponse Withdraw(string userId, string accountId, decimal amount);
    }
}