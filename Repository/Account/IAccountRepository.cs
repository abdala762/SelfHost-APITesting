namespace Repository.Account
{
    public interface IAccountRepository
    {
        double? GetAccountBalance(int accountNumber);
    }
}
