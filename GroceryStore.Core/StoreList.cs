using System;
using System.Collections.Generic;

namespace GroceryStore.Core
{
    public class StoreList
    {
        public List<Product> Products { get; set; }

        public double VAT { get; private set; }

        public bool AddProduct(Product product)
        {
            Products.Add(product);
            return true;
        }

        public List<Product> GetProducts()
        {
            return Products;
        }

        public bool ReduceProductQuantity(string id, int quantity)
        {
            bool wasProductRemoveSuccessfully = false;

            var product = Products.Find(prod => prod.Id == id);

            if (product != null)
            {
                product.Quantity -= quantity;
                wasProductRemoveSuccessfully = true;
            }

            return wasProductRemoveSuccessfully;
        }

        public void ReduceProductQuantityOnCheckOut(List<Product> cart)
        {
            foreach (var prod in cart)
            {
                int index = Products.FindIndex(item => item.Id == prod.Id);

                if (index > -1)
                {
                    Products[index].Quantity += prod.Quantity;
                }
            }

        }

        public bool RemoveProduct(string id)
        {
            bool wasProductRemovalSuccessful = false;

            var product = Products.Find(prod => prod.Id == id);

            if (product != null)
            {
                Products.Remove(product);
                wasProductRemovalSuccessful = true;
            }

            return wasProductRemovalSuccessful;
        }

        public void SetVAT(double newVAT)
        {
            VAT = newVAT;
        }

        public bool UpdateProductPrice(string id, decimal price)
        {
            bool wasPriceUpdateSuccessful = false;

            var product = Products.Find(prod => prod.Id == id);

            if (product != null)
            {
                product.Price = price;
                wasPriceUpdateSuccessful = true;
            }

            return wasPriceUpdateSuccessful;
        }
    }
}
