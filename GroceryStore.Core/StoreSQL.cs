using GroceryStore.Core.Contracts;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GroceryStore.Core
{
    public class StoreSQL : IStore
    {
        SqlCommand cmd;
        SqlDataReader reader;
        SqlConnection conn;
        string sql;

        private readonly string _conn = @"Data Source=DESKTOP-9RCAP09\SQLEXPRESS;Initial Catalog=GroceryStore;Integrated Security=True";
        public List<Product> Products { get; set; }

        public double VAT { get; private set; }

        public StoreSQL()
        {
            Products = GetProducts();
        }

        public bool AddProduct(Product product)
        {
            bool wasProductAddedSuccessfully = false;

            sql = $"insert into products values ('{product.Id}', '{product.Name}', {product.Price}, {product.Quantity})";

            using (conn = new SqlConnection(_conn))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    wasProductAddedSuccessfully = true;
                }
            }

            return wasProductAddedSuccessfully;
        }

        public List<Product> GetProducts()
        {
            sql = "select * from products";

            var result = new List<Product>();

            using (conn = new SqlConnection(_conn))
            {
                cmd = new SqlCommand(sql, conn);

                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(
                            new Product(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetInt32(3)
                            )
                            {
                                Price = reader.GetInt32(2)
                            }
                        );
                    }
                }
            }

            Products = result;
            return result;
        }

        public bool ReduceProductQuantity(string id, int quantity)
        {
            bool wasQuantityReducedSuccessfully = false;

            sql = $"update products set quantity = quantity - {quantity} where Id='{id}'";

            using (conn = new SqlConnection(_conn))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    wasQuantityReducedSuccessfully = true;
                }
            }

            return wasQuantityReducedSuccessfully;
        }

        public void ReduceProductQuantityOnCheckOut(List<Product> cart)
        {
            foreach (var item in cart)
            {
                sql = $"update products set quantity = quantity - {item.Quantity} where Id = '{item.Id}'";
                using (conn = new SqlConnection(_conn))
                {
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool RemoveProduct(string id)
        {
            bool wasProductRemovalSuccessful = false;

            sql = $"delete from products where Id='{id}'";

            using (conn = new SqlConnection(_conn))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    wasProductRemovalSuccessful = true;
                }
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

            sql = $"update products set price = {price} where Id = '{id}'";

            using (conn = new SqlConnection(_conn))
            {
                cmd = new SqlCommand(sql, conn);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    wasPriceUpdateSuccessful = true;
                }
            }

            return wasPriceUpdateSuccessful;
        }
    }
}

