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
    public class Users:BasePromotion
    {
       // private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        Dictionary<int, User> dictionsryUser = new Dictionary<int, User>();
     
        public Users(Logger Log) : base(Log)
        {
            
        }
        string connectionString = Environment.GetEnvironmentVariable("connectionString");
        public void CreateUsers(string userName, string cellphoneNumber, string email, string UserType, string twitterUsername, string user_id)
        {
           log.LogEvent("CreateUsers function called" ,DateTime.Now);

            // Define the insert statement
            string insert = "insert into Usertable (userName, cellphoneNumber, twitterUsername, email, UserType,user_id) OUTPUT INSERTED.userId values (@userName, @cellphoneNumber, @twitterUsername, @email, @UserType,@user_id)";

            try
            {
                // Open a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Create a command with the insert statement and the connection
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        if (string.IsNullOrWhiteSpace(userName))
                            throw new ArgumentException("userName cannot be null or empty", nameof(userName));
                        if (string.IsNullOrWhiteSpace(twitterUsername))
                            throw new ArgumentException("twitterUsername cannot be null or empty", nameof(twitterUsername));
                        // Open the connection
                        connection.Open();

                        // Add the user message data as parameters to the command
                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@cellphoneNumber", cellphoneNumber);
                        command.Parameters.AddWithValue("@twitterUsername", twitterUsername);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@UserType", UserType);
                        command.Parameters.AddWithValue("@user_id", user_id);

                        int userId = (int)command.ExecuteScalar();
                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                // Handle SQL exceptions
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                // Handle other exceptions
                throw;
            }
        }



        public Dictionary<int, User> UserDetailsfromSQL()
        {
          log.LogEvent(" UserDetailsfromSQL function called",DateTime.Now);
            try
            {
                CampaingSql CampaingSql = new CampaingSql(base.log);
                Dictionary<int, User> ret;
                CampaingSql.LoadingCampingsDetails("select * from Usertable", ReadUsersFromDb);

                return dictionsryUser;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
                return null;
            }



        }


        public void ReadUsersFromDb(SqlDataReader reader)
        {
           log.LogEvent("ReadUsersFromDb function called",DateTime.Now);
            //Clear Dictionary Before Inserting Information From Sql Server
            dictionsryUser.Clear();


            while (reader.Read())
            {
                User readUser = new User();

                readUser.userId = reader.GetInt32(0);
                readUser.userName = reader.GetString(1);
                readUser.cellphoneNumber = reader.GetString(2);

                readUser.twitterUsername = reader.GetString(3);
                readUser.email = reader.GetString(4);
                readUser.UserType = reader.GetString(5);


                //Cheking If Hashtable contains the key
                if (dictionsryUser.ContainsKey(readUser.userId))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    dictionsryUser.Add(readUser.userId, readUser);
                }




            }


        }
        public User GetUserSocialActivistFromDbById(string id)
        {
             log.LogEvent(" GetUserSocialActivistFromDbById function called",DateTime.Now);
            data.sql.UserSql userFromSql = new data.sql.UserSql(log);
            User socialActivistNew = (User)userFromSql.LoadOneSocialActivist(id);
            return socialActivistNew;
        }

    }
}
