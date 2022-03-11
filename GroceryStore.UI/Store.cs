using GroceryStore.Core;
using GroceryStore.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryStore.UI
{
    public partial class Store : Form
    {
        IStore ProductStore { get; set; }
        public Store()
        {
            InitializeComponent();
        }

        public Store(IStore store) : this()
        {
            ProductStore = store;
            RenderProducts();
        }

        public void RenderProducts()
        {
            DgvCart.Rows.Clear();
            foreach(var item in ProductStore.Products)
            {
                DgvProducts.Rows.Add(item.Id, item.Name, item.Quantity, item.Price);
            }
        }

        public void AddProduct()
        {
            var product = new Product(TxtProductName.Text, (int)NUDQuantity.Value) { Price = NUDPrice.Value };
            bool isAdded = ProductStore.AddProduct(product);
            if (!isAdded)
            {
                MessageBox.Show("Invalid Product Details", "Error");
                return;
            }
            MessageBox.Show("Product successfull added");
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct();
            ProductStore.GetProducts();
            RenderProducts();
        }
    }
}
