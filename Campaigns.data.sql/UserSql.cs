using Campaigns.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Utilitis;

namespace Campaigns.data.sql
{
 
    public class UserSql:BaseSql
    {
        

        public UserSql(Logger Log) : base(Log)
        {
        }

        public object LoadOneSocialActivist(string user_id)
        {
            var connectionString = Environment.GetEnvironmentVariable("connectionString");
            log.LogEvent("LoadOneSocialActivist function called with user_id",DateTime.Now);
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
            return user;
        }
    }
}
