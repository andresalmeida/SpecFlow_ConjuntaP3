using SpecFlowConjunta.Models;
using SpecFlowConjunta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SpecFlowConjunta.Data
{
    public class ClienteSQLDataAccessLayer
    {
        private readonly string connectionString = "Data Source=DESKTOP-9QK0T32\\SQLPRO;Initial Catalog=ProductosDB;User ID=sa;Password=password";

        // Create
        public int AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Create");
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", (object)product.Category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                con.Open();
                int newProductId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                return newProductId;
            }
        }

        // Read (Get all products)
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Read");

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                        ProductName = rdr["ProductName"].ToString(),
                        Category = rdr["Category"] == DBNull.Value ? null : rdr["Category"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"]),
                        StockQuantity = Convert.ToInt32(rdr["StockQuantity"])
                    });
                }
                con.Close();
            }
            return products;
        }

        // Read (Get a specific product)
        public Product GetProductById(int productId)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Read");
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                        ProductName = rdr["ProductName"].ToString(),
                        Category = rdr["Category"] == DBNull.Value ? null : rdr["Category"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"]),
                        StockQuantity = Convert.ToInt32(rdr["StockQuantity"])
                    };
                }
                con.Close();
            }
            return product;
        }

        // Update
        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", (object)product.Category ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Delete
        public void DeleteProduct(int productId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // By Name
        public Product GetProductByName(string productName)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductsCRUD", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "ReadByName");
                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    int productId = Convert.ToInt32(rdr["ProductId"]);

                    // Ahora usa el ID para obtener el producto completo
                    product = GetProductById(productId);
                }
                con.Close();
            }

            return product;
        }

    }
}