namespace ExampleAPI
{
    public class Account
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; } 


        public Account(string userId, decimal balance)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            Balance = balance;
        }
    }
}