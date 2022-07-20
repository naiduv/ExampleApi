namespace ExampleAPI
{
    public static class Constants
    {
        public static readonly decimal _minimumAmount = 100;

        public static readonly decimal _maxPerTransactionWithdrawalPercent = 90;

        public static readonly decimal _maxDepositAmount = 10000;

        public static readonly string _minimumAmountMessage = "Account balance cannot be lower than " + _minimumAmount;

        public static readonly string _maxDepositAmountMessage = "Cannot deposit more than " + _maxDepositAmount + " in a single transaction";

        public static readonly string _maxPerTransactionWithdrawalPercentMessage = "Cannot withdraw more than " + _maxPerTransactionWithdrawalPercent + " percent of total balance ";

        public static readonly string _userRequiredToCreateAccount = "Account creation requires a userId";

        public static readonly string _depositWithdrawFunctionError = "Please specify Deposit or Withdraw as function";

        public static readonly string _accountCreateSuccess = "Account was created Successfully";

        public static readonly string _accountDeleteSuccess = "Account was deleted Successfully";

        public static readonly string _accountWithdrawalSuccess = "Successfully Withdrew from Account";

        public static readonly string _accountWithdrawalFailureUserMismatch = "Could not Withdraw from Account, user does not match";

        public static readonly string _accountDepositSuccess = "Successfully Deposited into Account";

        public static readonly string _accountDeleteFailureUserMismatch = "Account could not be deleted because user does not match";

        public static readonly string _accountDoesNotExist = "Account does not exist";

    }
}
