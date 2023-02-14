using Campaigns.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campaigns.Model;
using System.Data.SqlClient;
using System.Security.Policy;
using Campaigns.data.sql;
using System.Configuration;
using Utilitis;

namespace Campaigns.Entities
{
    public class sales:BasePromotion
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");
        public void InsertNewSaleToDb(string buyerName, string cellphoneNumber, string Email, string buyerAddress, string CompanyName, int productsId)
        {
           // logger.Info("InsertnewSaleseToDb function called reader: {0} {1} {2} {3} {4} ", buyerName, cellphoneNumber, Email, buyerAddress, CompanyName);
            // SQL insert statement to insert a new row into the Donation table
            string insert = "insert into Salese values (@buyerName,@cellphoneNumber,@Email,@buyerAddress ,@CompanyName)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        connection.Open();

                        // Add the donation data as parameters to the command
                        command.Parameters.AddWithValue("@buyerName", buyerName);
                        command.Parameters.AddWithValue("@cellphoneNumber", cellphoneNumber);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@buyerAddress", buyerAddress);
                        command.Parameters.AddWithValue("@CompanyName", CompanyName);


                        // Execute the command
                        command.ExecuteNonQuery();
                        int productNumber = productsId; // product number you want to check
                        int userMoney;
                        int price;

                        // Use a SqlCommand to execute the SELECT query
                        using (SqlCommand selectCommand = new SqlCommand("SELECT [Price], [BuyerName], [CampaignName], [Twitter].[userMoney] " +
                                                                         "FROM [dbo].[Sales] " +
                                                                         "INNER JOIN [dbo].[Donation] " +
                                                                         "ON [dbo].[Donation].[productsId] = [dbo].[Sales].[Product_Number] " +
                                                                         "INNER JOIN [dbo].[Twitter] " +
                                                                         "ON [dbo].[Twitter].[email] = [dbo].[Sales].[Email] " +
                                                                         "WHERE [dbo].[Sales].[Product_Number] = @Product_Number", connection))
                        {
                            selectCommand.Parameters.AddWithValue("@Product_Number", productNumber);

                            // Use a SqlConnection and SqlCommand to execute the insert statement

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    price = (int)reader["Price"];
                                    userMoney = (int)reader["userMoney"];
                                    if (userMoney > price)
                                    {
                                        userMoney -= price;

                                        using (SqlCommand updateCommand = new SqlCommand("UPDATE [dbo].[Products] " +
                                                  "SET [Active] = 0   " +
                                                  "WHERE [Product_Number] = @Product_Number", connection))
                                        {
                                            updateCommand.Parameters.AddWithValue("@Product_Number", productNumber);
                                            updateCommand.ExecuteNonQuery();
                                        }

                                        // Use a new SqlCommand to execute the update statement
                                        using (SqlCommand updateCommand = new SqlCommand("UPDATE [dbo].[Twitter] " +
                                                                                        "SET [userMoney] = @userMoney   " +
                                                                                        "WHERE [email] = @email", connection))
                                        {
                                            updateCommand.Parameters.AddWithValue("@userMoney", userMoney);
                                            updateCommand.Parameters.AddWithValue("@email", reader["Email"]);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                    }
                     }
                 }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                Console.WriteLine(ex.Message);
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                throw;
            }
            catch (Exception ex)
            {
            
                // Handle other exceptions
                Console.WriteLine(ex.Message);
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                throw;
            }
        }














        public async Task TweetSale()


        {
            // Replace with your own Twitter API credentials
            var consumerKey = "NroY2hqa8za6hOxcxKpZp0myc";
            var consumerSecret = "bhcioubEFoDZvoWmaZR9ymFXGUMx4LzuI2NO71uiLTv9CDE36o";
            var accessToken = "1611075010822561797-XgnmEhFvvxvKj9kZnGfscYpTktucbk";
            var accessTokenSecret = "VrasnMznSoAygOtMJxGybTlnZdeRs5RA19E2Utmr6Ow3w";

            // Authenticate with Twitter


            var userClient = new Tweetinvi.TwitterClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            // request the user's information from Twitter API
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            Console.WriteLine("Hello " + user);

            // publish a tweet
            var tweet = await userClient.Tweets.PublishTweetAsync("New product sold");
            Console.WriteLine("You published the tweet : " + tweet);




        }









        List<Sale> listSales = new List<Sale>();

        // Method to process the retrieved data and fill the list with sale objects
        public void ReadSalesDetailsFromDb(SqlDataReader reader)
        {
          //  logger.Info("ReadSalesDetailsFromDb function called");
            try
            {
                // Clear the list before inserting data from the database
                listSales.Clear();

                // Read each row in the data reader
                while (reader.Read())
                {
                    // Create a new sale object
                    Sale readListSalesDetails = new Sale();

                    // Initialize the fields of the sale object with data from the database
                    readListSalesDetails.productsId = reader.GetInt32(0);
                    readListSalesDetails.buyerName = reader.GetString(1);
                    readListSalesDetails.cellphoneNumber = reader.GetString(2);
                    readListSalesDetails.Email = reader.GetString(3);
                    readListSalesDetails.buyerAddress = reader.GetString(4);

                    readListSalesDetails.CompanyName = reader.GetString(5);
                    readListSalesDetails.Product = reader.GetString(6);
                    readListSalesDetails.Price = reader.GetString(7);
                    readListSalesDetails.CampaignName = reader.GetString(8);
                    //readListSalesDetails.DATE = reader.GetDateTime(9);

                    // Add the sale object to the list
                    listSales.Add(readListSalesDetails);
                }
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                throw;
            }
        }




        public List<Sale> GetSalesDetails()
        {
            log.LogEvent("shippingDetailsfromSQL function called",DateTime.Now);
            try
            {
                CampaingSql CampaingSql = new CampaingSql(base.log);

                // Call the LoadingCampingsDetails method to execute the SQL query
                // and pass the ReadSalesDetails method as a callback
                CampaingSql.LoadingCampingsDetails(" SELECT [Product_Number], [buyerName], [cellphoneNumber], [dbo].[Donation].[Email],\r\n[buyerAddress], [dbo].[Donation].[CompanyName], [Product], [Price], [CampaignName],\r\ngetDate() as DATE FROM [dbo].[Donation] JOIN [dbo].[Salese] ON [dbo].[Donation].[CompanyName] = [dbo].[Salese].[CompanyName] ", ReadSalesDetails);


                // Return the list of sales
                return listSale;
            }
            catch (Exception ex)
            {
              
                // Print the exception message if an error occurs
                Console.WriteLine(ex.Message);
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                return null;
            }
        }

        private List<Sale> listSale = new List<Sale>();

        public sales(Logger Log) : base(Log)
        {
        }

        // Method to process the retrieved data and fill the list with sale objects
        private void ReadSalesDetails(SqlDataReader reader)
        {
            log.LogEvent("ReadSalesDetails function called",DateTime.Now);
            try
            {
                // Clear the list before inserting data from the database
                listSale.Clear();

                // Read each row in the data reader
                while (reader.Read())
                {
                    // Create a new sale object
                    Sale sale = new Sale();

                    // Initialize the fields of the sale object with data from the database
                    sale.productsId = reader.GetInt32(0);
                    sale.buyerName = reader.GetString(1);
                    sale.cellphoneNumber = reader.GetString(2);
                    sale.Email = reader.GetString(3);
                    sale.buyerAddress = reader.GetString(4);
                    sale.CompanyName = reader.GetString(5);
                    sale.Product = reader.GetString(6);
                    sale.Price = reader.GetString(7);
                    sale.CampaignName = reader.GetString(8);


                    // Add the sale object to the list
                    listSale.Add(sale);
                }
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                // Print the exception message if an error occurs
                Console.WriteLine(ex.Message);
            }
        }



        public List<Sale> GetmoneyShappingDbById(string id)
        {
            log.LogEvent("GetMoneyTrackingsFromDbById function called reader: " + id, DateTime.Now);
            data.sql.SaleSql SaleFromSql = new data.sql.SaleSql(log);
            List<Sale> SaleList = SaleFromSql.LoadAllSale(id);
            return SaleList;
        }



        public List<Sale> GetSaleFromDbById(int id)
        {
            data.sql.SaleSql userFromSql = new data.sql.SaleSql(log);
            return userFromSql.LoadSales(id);
        }


        public List<Sale> GetShippingFromDbById(string id)
        {

            data.sql.SaleSql userFromSql = new data.sql.SaleSql(log);
            List<Sale> shipping = userFromSql.LoadShipping(id);
            return shipping;
        }
    }

}
