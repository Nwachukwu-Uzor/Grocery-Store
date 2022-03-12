using GroceryStore.Core.Contracts;
using GroceryStore.Core.Enums;

namespace GroceryStore.Core
{
    public class User : IUser
    {
        public string Name { get; }
        public string Password { get; }
        public Role Role { get; }

        public User(string name, string password, string role)
        {
            Name = name;
            Password = password;
            Role = role == "Admin" ? Role.Admin : Role.Staff;
        }
    }
}
