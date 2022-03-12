using GroceryStore.Core;
using GroceryStore.Core.Contracts;
using System;
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
            DgvProducts.Rows.Clear();
            foreach(var item in ProductStore.Products)
            {
                DgvProducts.Rows.Add(item.Id, item.Name, item.Quantity, item.Price);
            }
        }

       public void RenderCart()
        {
            var sn = 0;
            DgvCart.Rows.Clear();
            foreach (var item in ProductStore.Cart.MyCart)
            {
                sn++;
                DgvCart.Rows.Add(sn, item.Name, item.Quantity, item.Price, item.Price * item.Quantity);
            }

            LblTotalPrice.Text = ProductStore.Cart.TotalPrice.ToString();
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

        private void BtnSell_Click(object sender, EventArgs e)
        {

            var wasProductAdded = ProductStore.AddProductToCart(TxtProductSellId.Text, int.Parse(LblQty.Text));

            if (!wasProductAdded)
            {
                MessageBox.Show("Invalid Product Id", "Error");
                return;
            }

            MessageBox.Show("Product Added SuccessFully", "Success");
            RenderCart();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var qty = int.Parse(LblQty.Text);
            qty++;
            LblQty.Text = qty.ToString();
        }

        private void BtnSubtract_Click(object sender, EventArgs e)
        {
            var qty = int.Parse(LblQty.Text);

            if (qty > 0)
            {
                qty--;
                LblQty.Text = qty.ToString();
                return;
            }
            MessageBox.Show("Product Quantity Cannot be Less Than 0", "Error");
        }
    }
}
