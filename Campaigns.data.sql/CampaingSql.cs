
using Campaigns.Dal;
using Campaigns.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Campaigns.Dal.SqlQuery;

namespace Campaigns.data.sql
{
    public class CampaingSql

    {

       // private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static string connectionString = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = campaign ; Data Source = localhost\\sqlEXPRESS";



        public Dictionary<int, Campaign> AddCampaignToDictionary(SqlDataReader reader)
        {
           // logger.Info("AddCampaignToDictionary function called with SqlDataReader: {0}", reader);

            Dictionary<int, Campaign> dictionaryCampaign = new Dictionary<int, Campaign>();

            while (reader.Read())
            {
                Campaign readCampaign = new Campaign
                {
                    userId = reader.GetInt32(reader.GetOrdinal("ProductID")),
                    associationName = reader.GetString(reader.GetOrdinal("associationName")),
                    email = reader.GetString(reader.GetOrdinal("email")),
                    uri = reader.GetString(reader.GetOrdinal("uri")),
                    hashtag = reader.GetString(reader.GetOrdinal("hashtag"))
                };

                dictionaryCampaign.Add(readCampaign.userId, readCampaign);
            }

            return dictionaryCampaign;
        }


        public static void LoadingCampingsDetails(string SqlQuery, SetDataReader_delegate1 Ptrfunc)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = SqlQuery;

                    // Adapter
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        //Reader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Ptrfunc(reader);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
             //   logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                // log the error message
              //  NLog.Fluent.Log.Error(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
               // logger.Error(ex, "Exception occurred: {0}", ex.Message);

                // log the error message

                throw;
            }
        }

        public void UpdateAProduct(int userId, string associationName, string email, string uri, string hashtag, string CampaignName)
        {


            string update = "update Campaigns set  associationName = @associationName, email = @email,uri=@uri,hashtag = @hashtag,CampaignName=@CampaignName where userId = @userId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@associationName", associationName);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@uri", uri);
                        command.Parameters.AddWithValue("@hashtag", hashtag);
                        command.Parameters.AddWithValue("@CampaignName", CampaignName);

                        //Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        public Dictionary<int, Donation> Add2cToDictionary(SqlDataReader reader)
        {
            //logger.Info("Add2cToDictionary function called with SqlDataReader: {0}", reader);
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
           // logger.Info("LoadOneProduct function called with productsId: {0}", productsId);

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
              //  logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
              //  logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the product object
            return product;
        }







        public List<Sale> LoadSales(int userId)
        {
          //  logger.Info("LoadSales function called with userId: {0}", userId);
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
               // logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
               // logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the product object
            return sales;
        }







        public object LoadOneSocialActivist(string user_id)
        {
            //logger.Info("LoadOneSocialActivist function called with user_id: {0}", user_id);
            // Define the SELECT statement that will be used to retrieve the product data.
            string select = "select * from Usertable where user_id = @user_id";

            // Create a new product object
            User user = new User();

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
                        command.Parameters.AddWithValue("@user_id", user_id);

                        // Execute the SELECT statement and get the result set
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Read the result set
                            while (reader.Read())
                            {



                                // Set the values of the product object with the data from the result set

                                user.userName = reader.GetString(reader.GetOrdinal("userName"));
                                user.cellphoneNumber = reader.GetString(reader.GetOrdinal("cellphoneNumber"));
                                user.email = reader.GetString(reader.GetOrdinal("email"));
                                user.UserType = reader.GetString(reader.GetOrdinal("UserType"));
                                user.user_id = reader.GetString(reader.GetOrdinal("user_id"));
                            }
                        }
                    }
                }
            }
            // Catch any SQL exceptions that may occur and write the error message to the console
            catch (SqlException ex)
            {
               // logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
            //    logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the product object
            return user;
        }


        public List<moneyTracking> LoadAllmoneyTrackings(string user_id)
        {
           // logger.Info("LoadAllmoneyTrackings function called with user_id: {0}", user_id);
            // Define the SELECT statement that will be used to retrieve the product data.
            string select = "select [user_id],[associationName],[hashtag],[dbo].[Twitter].[userName],[userMoney]from [dbo].[Twitter] inner join [dbo].[Usertable] on [dbo].[Usertable].[email]=[dbo].[Twitter].[email] where [user_id]=@user_id";

            // Create a new list to store the moneyTracking objects
            List<moneyTracking> moneyTrackings = new List<moneyTracking>();

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
                                moneyTracking moneyTracking = new moneyTracking();
                                // Set the values of the moneyTracking object with the data from the result set
                                moneyTracking.user_id = reader.GetString(reader.GetOrdinal("user_id"));
                                moneyTracking.associationName = reader.GetString(reader.GetOrdinal("associationName"));
                                moneyTracking.hashtag = reader.GetString(reader.GetOrdinal("hashtag"));
                                moneyTracking.userName = reader.GetString(reader.GetOrdinal("userName"));
                                moneyTracking.userMoney = reader.GetInt32(reader.GetOrdinal("userMoney"));
                                // Add the moneyTracking object to the list
                                moneyTrackings.Add(moneyTracking);
                            }
                        }
                    }
                }
            }
            // Catch any SQL exceptions that may occur and write the error message to the console
            catch (SqlException ex)
            {
               // logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
               // logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the list of moneyTracking objects
            return moneyTrackings;
        }




        public List<Sale> LoadShipping(string user_id)
        {
          //  logger.Info("LoadAllmoneyTrackings function called with user_id: {0}", user_id);
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
              //  logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
              //  logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the list of moneyTracking objects
            return sales;
        }




        public List<Campaign> LoadCampaign(string user_id)
        {

            string select = "SELECT * FROM  [dbo].[Campaigns]\r\nJOIN [dbo].[Usertable] ON [dbo].[Usertable].email = [dbo].[Campaigns].email where [user_id]=@user_id";

            // Create a new list to store the moneyTracking objects
            List<Campaign> campaigns = new List<Campaign>();

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
                                Campaign campaign = new Campaign();
                                // Set the values of the moneyTracking object with the data from the result set
                                campaign.userId = reader.GetInt32(0);
                                campaign.associationName = reader.GetString(1);
                                campaign.email = reader.GetString(2);
                                campaign.uri = reader.GetString(3);
                                campaign.hashtag = reader.GetString(4);
                                campaign.CampaignName = reader.GetString(5);


                                campaigns.Add(campaign);
                            }
                        }
                    }
                }
            }
            // Catch any SQL exceptions that may occur and write the error message to the console
            catch (SqlException ex)
            {
               // logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
             //   logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the list of moneyTracking objects
            return campaigns;




        }
        public void DeleteCampaig(int userId)
        {


            string delete = "delete from Campaigns where userId = @userId ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        connection.Open();

                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Sale> LoadAllSale(string user_id)
        {
           // logger.Info("LoadAllmoneyTrackings function called with user_id: {0}", user_id);
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
               // logger.Error(ex, "SQL exception occurred: {0}", ex.Message);
                Console.WriteLine(ex.Message);
                throw;
            }
            // Catch any other exceptions that may occur and write the error message to the console
            catch (Exception ex)
            {
              //  logger.Error(ex, "Exception occurred: {0}", ex.Message);

                Console.WriteLine(ex.Message);
                throw;
            }

            // Return the list of moneyTracking objects
            return Sales;
        }
    }
}
