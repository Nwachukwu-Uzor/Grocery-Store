using GroceryStore.Core.Enums;

namespace GroceryStore.Core.Contracts
{
    public interface IUser
    {
        string Name { get; set; }
        string Password { get; set; }
        Role Role { get; set; }
    }
}
