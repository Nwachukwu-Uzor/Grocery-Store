using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryStore.Core.Contracts
{
    public interface IProduct
    {
        string Id { get; }
        string Name { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }
}
