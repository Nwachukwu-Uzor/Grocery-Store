using System;
using System.Windows.Forms;

using GroceryStore.Core;
using GroceryStore.Core.Contracts;
using GroceryStore.Commons;
using System.Text;

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
            ProductStore.GetProducts();
            foreach (var item in ProductStore.Products)
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
            ClearInputs();
            RenderProducts();
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            AddProduct();
            ProductStore.GetProducts();
            RenderProducts();
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {

            var wasProductAdded = ProductStore.AddProductToCart(TxtProductSellId.Text, int.Parse(LblQty.Text));

            if (!wasProductAdded)
            {
                MessageBox.Show("Invalid Product Id", "Error");
                return;
            }

            LblTotalPrice.Text = ProductStore.Cart.TotalPrice.ToString();
            MessageBox.Show("Product Added SuccessFully", "Success");
            ClearInputs();
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

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            string receiptContent = PopulateReceiptContent();
            string formatEnd = Guid.NewGuid().ToString().Substring(0, 10);
            string printPath = $@"C:\Users\UzorNwachukwu\Documents\C# Playground\GroceryStoreReceipts\receipt{formatEnd}.txt";

            if (receiptContent != "")
            {
                receiptContent += $"\n Total Price: {ProductStore.Cart.TotalPrice}";
                FileHandler.PrintFile(receiptContent, printPath);
                ProductStore.ReduceProductQuantityOnCheckOut(ProductStore.Cart.MyCart);
                MessageBox.Show($"Receipt successfull printed to {printPath}");
                ProductStore.Cart.ClearCart();
                RenderCart();
                RenderProducts();
                return;
            }

            MessageBox.Show("Cart is empty");
        }

        private string PopulateReceiptContent()
        {
            var receiptContent = new StringBuilder();
            int count = 0;

            foreach (var item in ProductStore.Cart.MyCart)
            {
                count++;
                receiptContent.AppendLine($"" +
                    $"{count} - Name: {item.Name} \r Quantity: {item.Quantity} \r Unit Price: {item.Price} \r" +
                    $"Total Price: {item.Price * item.Quantity}");
            }

            return receiptContent.ToString();
        }

        private void ClearInputs()
        {
            TxtProductName.Text = "";
            NUDPrice.Value = 0;
            NUDQuantity.Value = 0;

            TxtProductSellId.Text = "";
            LblQty.Text = "0";

            TxtProductId.Text = "";
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtProductName.Text = "";
            NUDPrice.Value = 0;
            NUDQuantity.Value = 0;
        }

        private void BtnClearRemove_Click(object sender, EventArgs e)
        {
            TxtProductId.Text = "";
        }

        private void BtnClearSell_Click(object sender, EventArgs e)
        {
            TxtProductSellId.Text = "";
            LblQty.Text = "0";
        }
    }
}
