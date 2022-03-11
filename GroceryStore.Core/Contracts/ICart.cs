using System.Collections.Generic;

namespace GroceryStore.Core.Contracts
{
    public interface ICart
    {
        List<Product> MyCart { get; set; }

        Product AddProduct(string id, string name, decimal price, int quantity);
        Product CheckProductInCart(string id);
    }
}
