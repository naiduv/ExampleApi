using ExampleAPI.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CreateFailsIfLessThan100()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;

            var accountService = new AccountService(logger);
            Assert.False(accountService.Create("test", 99).Result);
        }

        [Fact]
        public void CreateSucceedsIfGreaterThan100()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;

            var accountService = new AccountService(logger);
            Assert.True(accountService.Create("test", 100).Result);
            Assert.True(accountService.Create("test", 101).Result);

        }

        [Fact]
        public void CreateAnyNumberOfAccountsForUser()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;

            var accountService = new AccountService(logger);
            for (int i = 0; i < 200; i++)
            {
                Assert.True(accountService.Create("test", 101).Result);
            }

        }

        [Fact]
        public void UserCanCreateAndDeleteAccount()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;

            var accountService = new AccountService(logger);
            var accountId = accountService.Create("test", 101).AccountId;
            Assert.True(accountService.Delete("test", accountId).Result);

            accountId = accountService.Create("test", 102).AccountId;
            Assert.True(accountService.Delete("test", accountId).Result);
        }

        [Fact]
        public void UserCannotDeleteAccountThatDoesNotExist()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;
            var accountService = new AccountService(logger);

            Assert.False(accountService.Delete("test", "xxxxx").Result);

        }

        [Fact]
        public void UserCanDepositAndWithdrawFromAccount()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;
            var accountService = new AccountService(logger);

            var accountId = accountService.Create("test", 101).AccountId;
            Assert.True(accountService.Deposit(accountId, 1000).Result);
            Assert.True(accountService.Deposit(accountId, 100).Result);

        }

        [Fact]
        public void UserCannotDepositMoreThan10000()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;
            var accountService = new AccountService(logger);

            var accountId = accountService.Create("test", 100001).AccountId;
            Assert.False(accountService.Deposit(accountId, 10001).Result);
        }


        [Fact]
        public void UserCannotWithdrawMoreThan90PercentOfBalance()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;
            var accountService = new AccountService(logger);

            var accountId = accountService.Create("test", 10000).AccountId;
            Assert.False(accountService.Withdraw("test", accountId, 9001).Result);
        }


        [Fact]
        public void UserMustMaintain100MinimumBalance()
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;
            var accountService = new AccountService(logger);

            var accountId = accountService.Create("test", 500).AccountId;
            Assert.True(accountService.Withdraw("test", accountId, 100).Result);
            Assert.True(accountService.Withdraw("test", accountId, 100).Result);
            Assert.True(accountService.Withdraw("test", accountId, 100).Result);
            Assert.True(accountService.Withdraw("test", accountId, 100).Result);
            Assert.False(accountService.Withdraw("test", accountId, 100).Result);

        }




        [Theory]
        [InlineData("test1", 99, false)]
        [InlineData("test2", 100, true)]
        [InlineData("", 1, false)]
        [InlineData("", 200, false)]
        public void CreateTest(string userId, int amount, bool result)
        {
            var mock = new Mock<ILogger<AccountService>>();
            ILogger<AccountService> logger = mock.Object;

            var accountService = new AccountService(logger);
            Assert.Equal(result, accountService.Create(userId, amount).Result);
        }

    }
}