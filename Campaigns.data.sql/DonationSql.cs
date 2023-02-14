using Campaigns.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilitis;

namespace Campaigns.data.sql
{
    public class DonationSql:BaseSql
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public DonationSql(Logger Log) : base(Log)
        {
        }

        public Dictionary<int, Donation> Add2cToDictionary(SqlDataReader reader)
        {
             log.LogEvent("Add2cToDictionary function called with SqlDataReader",DateTime.Now);
            //Create a dictionary that will contain the products data. The key of the dictionary is the product's ID and the value is the Product object
            Dictionary<int, Donation> dictionsryProducts = new Dictionary<int, Donation>();

            //Clear the dictionary before adding new products.
            dictionsryProducts.Clear();

            while (reader.Read())
            {
                Donation readProducts = new Donation();

                readProducts.productsId = reader.GetInt32(reader.GetOrdinal("productsId"));
                readProducts.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                readProducts.Product = reader.GetString(reader.GetOrdinal("Product"));
                readProducts.Email = reader.GetString(reader.GetOrdinal("Email"));
                readProducts.Price = reader.GetString(reader.GetOrdinal("Price"));




                //Add the new Product object to the dictionary
                dictionsryProducts.Add(readProducts.productsId, readProducts);
            }

            return dictionsryProducts;
        }
        public object LoadOneProduct(int productsId)
        {
            log.LogEvent("LoadOneProduct function called with productsId",DateTime.Now);

            // Define the SELECT statement that will be used to retrieve the product data
            string select = "select * from Donation where productsId = @productsId";

            // Create a new product object
            Donation product = new Donation();

            try
            {
                // Create a new connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a new command that will be used to execute the SELECT statement
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        // Open the connection
                        connection.Open();

                        // Add the product ID parameter to the SELECT statement
                        command.Parameters.AddWithValue("@productsId", productsId);

                        // Execute the SELECT statement and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the result set
                            while (reader.Read())
                            {
                                // Set the values of the product object with the data from the result set
                                product.productsId = reader.GetInt32(reader.GetOrdinal("productsId"));
                                product.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));
                                product.Product = reader.GetString(reader.GetOrdinal("Product"));
                                product.Email = reader.GetString(reader.GetOrdinal("Email"));
                                product.Price = reader.GetString(reader.GetOrdinal("Price"));
                            }
                        }
                    }
                }
            }
            // Catch any SQL exceptions that may occur and write the error message to the console
            catch (SqlException ex)
            {
                log.LogException("SQL exception occurred", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
                log.LogException("SQL exception occurred", ex, DateTime.Now);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the product object
            return product;
        }


    }
}
