using GroceryStore.Core.Enums;

namespace GroceryStore.Core.Contracts
{
    public interface IUser
    {
        string Name { get; }
        string Password { get; }
        Role Role { get; }
    }
}
