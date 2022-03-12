using GroceryStore.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Core
{
  
    public class Cart : ICart
    {
        public List<Product> MyCart { get; set; }
        public decimal TotalPrice { get; private set; }
        public Cart(List<Product> pCart)
        {
            MyCart = pCart;
        }

        public Product CheckProductInCart(string id)
        {
            Product product = MyCart.Find(item => item.Id == id);
            return product ?? null;
        }

        public void AddProduct(string id, string name, decimal price, int quantity)
        {
            string trimmedId = id.Trim();
            Product product = CheckProductInCart(trimmedId);

            if (product == null)
            {
                Product prod = new Product(id, name, quantity) { Price = price };
                MyCart.Add(prod);
                return;
            }
                
            product.Quantity += quantity;

            TotalPrice = CalculateTotalPrice();
        }

        public decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;

            foreach(var item in MyCart)
            {
                totalPrice += item.Quantity * item.Price;
            }

            return totalPrice;
        }

        public void ClearCart()
        {
            MyCart.Clear();
        }
    }
}
