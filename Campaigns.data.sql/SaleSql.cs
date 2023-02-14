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
    public class SaleSql:BaseSql
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public SaleSql(Logger Log) : base(Log)
        {
        }

        public List<Sale> LoadSales(int userId)
        {
            log.LogEvent("LoadSales function called with userId",DateTime.Now);
            // Define the SELECT statement that will be used to retrieve the product data
            string select = "SELECT [buyerName],[Salese].[cellphoneNumber], [dbo].[Donation].[Email],[buyerAddress], [dbo].[Donation].[CompanyName], [Product], [Price], [CampaignName],getDate() as DATE FROM [dbo].[Donation] \r\nJOIN [dbo].[Salese] ON [dbo].[Donation].[CompanyName] = [dbo].[Salese].[CompanyName]\r\nJOIN Users ON [Donation].[Email]=Users.[email]\r\nWHERE [userId] = @userId";

            // Create a new product object
            List<Sale> sales = new List<Sale>();

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
                        command.Parameters.AddWithValue("@userId", userId);

                        // Execute the SELECT statement and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the result set
                            while (reader.Read())
                            {
                                Sale sale = new Sale();
                                sale.buyerName = reader.GetString(reader.GetOrdinal("buyerName"));
                                sale.cellphoneNumber = reader.GetString(reader.GetOrdinal("cellphoneNumber"));
                                sale.Email = reader.GetString(reader.GetOrdinal("Email"));
                                sale.buyerAddress = reader.GetString(reader.GetOrdinal("buyerAddress"));
                                sale.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                                sale.Product = reader.GetString(reader.GetOrdinal("Product"));
                                sale.Price = reader.GetString(reader.GetOrdinal("Price"));
                                sale.CampaignName = reader.GetString(reader.GetOrdinal("CampaignName"));

                                sales.Add(sale);
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
            return sales;
        }

        public List<Sale> LoadAllSale(string user_id)
        {
             log.LogEvent("LoadAllmoneyTrackings function called with user_id",DateTime.Now);
            // Define the SELECT statement that will be used to retrieve the product data.
            string select = "\r\nSELECT buyerName,[dbo].[Salese].cellphoneNumber, [dbo].[Salese].[Email], buyerAddress, [dbo].[Donation].[CompanyName], \r\nProduct, Price, CampaignName\r\nFROM [dbo].[Salese] \r\nINNER JOIN [dbo].[Donation] ON [dbo].[Donation].[CompanyName]=[dbo].[Salese].[CompanyName] \r\nINNER JOIN [dbo].[Usertable] ON [dbo].[Salese].Email=[dbo].[Usertable].email \r\nWHERE [user_id]=@user_id";

            // Create a new list to store the moneyTracking objects
            List<Sale> Sales = new List<Sale>();

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

                        // Add the user_id parameter to the SELECT statement
                        command.Parameters.AddWithValue("@user_id", user_id);

                        // Execute the SELECT statement and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the result set
                            while (reader.Read())
                            {
                                // Create a new moneyTracking object
                                Sale Sale = new Sale();
                                // Set the values of the moneyTracking object with the data from the result set

                                Sale.buyerName = reader.GetString(0);
                                Sale.cellphoneNumber = reader.GetString(1);
                                Sale.Email = reader.GetString(2);
                                Sale.buyerAddress = reader.GetString(3);
                                Sale.CompanyName = reader.GetString(4);
                                Sale.Product = reader.GetString(5);
                                Sale.Price = reader.GetString(6);
                                Sale.CampaignName = reader.GetString(7);

                                // Add the moneyTracking object to the list
                                Sales.Add(Sale);
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

            // Return the list of moneyTracking objects
            return Sales;
        }

        public List<Sale> LoadShipping(string user_id)
        {
            log.LogEvent("LoadAllmoneyTrackings function called with user_id", DateTime.Now);
            // Define the SELECT statement that will be used to retrieve the product data.
            string select = "SELECT [Product_Number],[buyerName],[Salese].[cellphoneNumber],[dbo].[Donation].[Email],[buyerAddress],[dbo].[Donation].[CompanyName],[Product],[Price],[CampaignName] \r\nFROM [dbo].[Donation] \r\nJOIN [dbo].[Salese] ON [dbo].[Donation].[CompanyName] = [dbo].[Salese].[CompanyName]\r\nJOIN [dbo].[Usertable] ON [dbo].[Donation].[Email] = [dbo].[Usertable].[email] where [user_id]= @user_id";

            // Create a new list to store the moneyTracking objects
            List<Sale> sales = new List<Sale>();

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

                        // Add the user_id parameter to the SELECT statement
                        command.Parameters.AddWithValue("@user_id", user_id);

                        // Execute the SELECT statement and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the result set
                            while (reader.Read())
                            {
                                // Create a new moneyTracking object
                                Sale sale = new Sale();
                                // Set the values of the moneyTracking object with the data from the result set
                                sale.productsId = reader.GetInt32(0);
                                sale.buyerName = reader.GetString(1);
                                sale.cellphoneNumber = reader.GetString(2);
                                sale.Email = reader.GetString(3);
                                sale.buyerAddress = reader.GetString(4);
                                sale.CompanyName = reader.GetString(5);
                                sale.Product = reader.GetString(6);
                                sale.Price = reader.GetString(7);
                                sale.CampaignName = reader.GetString(8);

                                sales.Add(sale);
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

            // Return the list of moneyTracking objects
            return sales;
        }
    }
}
