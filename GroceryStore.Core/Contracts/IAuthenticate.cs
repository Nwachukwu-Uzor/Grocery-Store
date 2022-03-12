namespace GroceryStore.Core.Contracts
{
    public interface IAuthenticate
    {
        User FindUser(string userName, string password);
    }
}
