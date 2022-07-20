namespace ExampleAPI
{
    public class AccountResponse
    {
        public string? AccountId { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }

        public AccountResponse(bool result, string message, string? id = null)
        {
            AccountId = id;
            Result = result;
            Message = message;
        }
    }
}