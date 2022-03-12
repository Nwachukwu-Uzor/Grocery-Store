using GroceryStore.Core.Contracts;
using GroceryStore.Core.Enums;
using System;

namespace GroceryStore.Core
{
    public class User : IUser
    {
        private readonly string _Id;

        public string Name { get; }
        public string Password { get; }
        public Role Role { get; }

        public User(string name, string role)
        {
            Name = name;
            Role = role == "Admin" ? Role.Admin : Role.Staff;
        }
        public User(string name, string password, string role) : this(name, role)
        { 
            _Id = Guid.NewGuid().ToString(); Name = name;
            Password = password;
        }
    }
}
