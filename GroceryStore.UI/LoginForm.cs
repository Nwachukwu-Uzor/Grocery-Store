using GroceryStore.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GroceryStore.UI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var cart = new Cart(new List<Product>());
            var productStore = new StoreSQL(cart);
            var store = new Store(productStore);
            store.Show();
        }
    }
}
