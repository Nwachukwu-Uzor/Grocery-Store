using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Core.Contracts
{
    public interface IStore
    {
        List<Product> Products { get; set; }
        ICart Cart { get; set; }
        double VAT { get; }
        List<Product> GetProducts();
        bool AddProduct(Product product);
        bool RemoveProduct(string id);
        bool ReduceProductQuantity(string id, int quantity);
        void ReduceProductQuantityOnCheckOut(List<Product> cart);
        bool UpdateProductPrice(string id, decimal price);

        bool AddProductToCart(string id, int qty);
        void SetVAT(double newVAT);
    }
}
