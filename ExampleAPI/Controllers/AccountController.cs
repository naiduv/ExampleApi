using ExampleAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet("GetAll")]
        public Dictionary<string, Account> Get()
        {
            return _accountService.All();
        }

        [HttpPost("Create")]
        public AccountResponse Create(string userId, decimal amount)
        {
            return _accountService.Create(userId, amount);
        }

        [HttpDelete("Delete")]
        public AccountResponse Delete(string userId, string accountId)
        {
            return _accountService.Delete(userId, accountId);
        }

        [HttpDelete("DeleteAll")]
        public Dictionary<string, Account> DeleteAll()
        {
            _accountService.DeleteAll();
            return _accountService.All();
        }


        [HttpPost("Withdraw")]
        public AccountResponse Withdraw(string userId, string accountId, decimal amount)
        {
            return _accountService.Withdraw(userId, accountId, amount);
        }

        [HttpPost("Deposit")]
        public AccountResponse Deposit(string accountId, decimal amount)
        {
            return _accountService.Deposit(accountId, amount);
        }

    }
}