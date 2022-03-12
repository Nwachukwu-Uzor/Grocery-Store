using GroceryStore.Core.Contracts;
using System.Data.SqlClient;

namespace GroceryStore.Core
{
    public class AuthenticateSQL : IAuthenticate
    {
        SqlCommand cmd;
        SqlDataReader reader;
        SqlConnection conn;
        string sql;

        private readonly string _conn = @"Data Source=DESKTOP-9RCAP09\SQLEXPRESS;Initial Catalog=GroceryStore;Integrated Security=True";
        public User FindUser(string name, string password)
        {
            sql = $"select [Name], Role from users where [Name] = '{name}' and Password = '{password}'";


            using (conn = new SqlConnection(_conn))
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        return new User(
                            reader.GetString(0),
                            reader.GetString(1)
                        );
                    }
                }
            }
            return null;
        }
    }
}
