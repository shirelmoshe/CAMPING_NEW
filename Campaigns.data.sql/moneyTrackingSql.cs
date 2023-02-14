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
    public class moneyTrackingSql:BaseSql
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public moneyTrackingSql(Logger Log) : base(Log)
        {
        }

        public List<moneyTracking> LoadAllmoneyTrackings(string user_id)
        {
            log.LogEvent("LoadAllmoneyTrackings function called with user_id", DateTime.Now);
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
            return moneyTrackings;
        }//

    }
}
