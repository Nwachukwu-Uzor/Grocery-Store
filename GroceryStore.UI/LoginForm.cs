using GroceryStore.Core;
using GroceryStore.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GroceryStore.UI
{
    public partial class LoginForm : Form
    {
        public IAuthenticate Authenticate { get; }
        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(IAuthenticate authenticate) : this()
        {
            Authenticate = authenticate;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var user = Authenticate.FindUser(TxtUsername.Text, TxtPassword.Text);

            if (user == null)
            {
                MessageBox.Show("Invalid Login Credentials", "Error");
                return;
            }

            var cart = new Cart(new List<Product>());
            var productStore = new StoreSQL(cart);
            var store = new Store(productStore, user);
            store.Show();
        }
    }
}
