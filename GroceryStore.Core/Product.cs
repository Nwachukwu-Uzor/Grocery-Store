using GroceryStore.Core.Contracts;
using System;

namespace GroceryStore.Core
{
    public class Product : IProduct
    {
        public Product(string productName, int quantity = 0)
        {
            Id = Guid.NewGuid().ToString().Substring(0, 10);
            Name = productName;
            Quantity = quantity;
        }

        public Product(string id, string productName, int quantity = 0)
        {
            Id = id;
            Name = productName;
            Quantity = quantity;
        }

        public string Id { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
