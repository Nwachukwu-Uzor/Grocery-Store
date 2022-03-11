using GroceryStore.Core;
using System;
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
            var productStore = new StoreSQL();
            var store = new Store(productStore);
            store.Show();
        }
    }
}
