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
        }

        public void RenderProducts()
        {
            foreach(var item in ProductStore.Products)
            {
                DgvProducts.Rows.Add(item.Id, item.Quantity, item.Price);
            }
        }
    }
}
