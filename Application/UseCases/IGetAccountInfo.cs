namespace Application.UseCases
{
    public interface IGetAccountInfo
    {
        AccountDto Execute(int accountNumber);
    }
}
