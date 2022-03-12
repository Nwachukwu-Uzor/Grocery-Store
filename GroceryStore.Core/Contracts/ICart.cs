using System.Collections.Generic;

namespace GroceryStore.Core.Contracts
{
    public interface ICart
    {
        List<Product> MyCart { get; set; }

        decimal TotalPrice { get; }
        void AddProduct(string id, string name, decimal price, int quantity);
        Product CheckProductInCart(string id);

        decimal CalculateTotalPrice();
        void ClearCart();
    }
}
