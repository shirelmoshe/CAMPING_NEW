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
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Utilitis;

namespace Campaigns.Entities
{

    public class Twitters:BasePromotion
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");
        List<Twitter> listTwitterTable;
        CampaingSql CampaingSql;

        public Twitters(Logger Log) : base(Log)
        {
            listTwitterTable = new List<Twitter>();
            CampaingSql = new CampaingSql(base.log);
        }

        public int userMoney = 0;
      
        string Twitter = ConfigurationManager.AppSettings["connectionString"];
        private static readonly string startTime = DateTime.UtcNow.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ssZ");
        private static readonly string endTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        private static readonly DateTime today = DateTime.Today.AddDays(-1);
        private static readonly DateTime dateOfnextDay = DateTime.Today;
        private static readonly string nextDay = today.ToString("yyyy-MM-dd");
        private static readonly string tomorrow = dateOfnextDay.ToString("yyyy-MM-dd");

        //  private static readonly string start_time = currentDay + "T00:00:00Z";
        //private static readonly string end_time = tomorrow + "T13:50:00Z";


        public void amountOftweets()
        {

            log.LogEvent("amountOftweets function called", DateTime.Now);
            List<Twitter> userData = ReadTwitterFromDb();

            foreach (Twitter user in userData)
            {
                DateTime startTime = DateTime.Now;
                if (user.lastCheckedDateTime != null)
                {
                    startTime = (DateTime)user.lastCheckedDateTime;
                }
                else
                {
                    startTime = startTime.AddDays(-7);
                }

                string url = $"https://api.twitter.com/2/tweets/search/recent?start_time={startTime}&end_time={endTime}&query=from:{user.twitterUsername}";

                var clientTwitter = new HttpClient();
                var requestTwitter = new HttpRequestMessage(HttpMethod.Get, url);
                requestTwitter.Headers.Add("authorization", Twitter);

                try
                {
                    var responseTwitter = clientTwitter.SendAsync(requestTwitter).Result;
                    if (responseTwitter.IsSuccessStatusCode)
                    {
                        var json = JObject.Parse(responseTwitter.Content.ReadAsStringAsync().Result);
                        int tweetCount = 0;
                        int resultCount = (int)json["meta"]["result_count"];
                        if (resultCount != 0)
                        {
                            foreach (var tweet in json["data"])
                            {
                                if (tweet["text"].ToString().Contains(user.hashtag))
                                {
                                    tweetCount++;
                                }
                            }
                        }


                        // Insert user data into the Twitter table

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            user.userMoney += tweetCount;
                            using (SqlCommand command = new SqlCommand("Update Twitter SET userMoney = @userMoney, lastCheckedDateTime = @lastCheckedDateTime WHERE twitterUsername = @twitterUsername AND hashtag=@hashtag ", connection))
                            {
                                command.Parameters.AddWithValue("@userMoney", user.userMoney);
                                command.Parameters.AddWithValue("@lastCheckedDateTime", DateTime.Now);
                                command.Parameters.AddWithValue("@twitterUsername", user.twitterUsername);
                                command.Parameters.AddWithValue("@hashtag", user.hashtag);

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error connecting to Twitter API: " + ex.Message);
                    throw;
                    // Log the error message
                }
                catch (JsonReaderException ex)
                {
                    log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                    Console.WriteLine("Error parsing JSON response: " + ex.Message);
                    // Log the error message
                    throw;
                }
                catch (Exception ex)
                {
                    log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                    Console.WriteLine("An unexpected error occurred: " + ex.Message);
                    // Log the error message
                    throw;
                }
            }
        }



        public List<Twitter> ReadTwitterFromDb()
        {
            log.LogEvent("ReadTwitterFromDb function called ", DateTime.Now);
            string select = "SELECT associationName, hashtag, email, userName, twitterUsername, CampaignName FROM Twitter";
            List<Twitter> TwitterList = new List<Twitter>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        connection.Open();

                        //Execute the command and store the results in a SqlDataReader
                        SqlDataReader reader = command.ExecuteReader();

                        //Read the data from the reader and store it in the campaignList
                        while (reader.Read())
                        {
                            TwitterList.Add(new Twitter
                            {
                                associationName = reader["associationName"].ToString(),
                                hashtag = reader["hashtag"].ToString(),
                                email = reader["email"].ToString(),
                                userName = reader["userName"].ToString(),
                                twitterUsername = reader["twitterUsername"].ToString(),
                                CampaignName = reader["CampaignName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                log.LogException("SQL exception occurred: {0}", ex, DateTime.Now);

                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);

                Console.WriteLine(ex.Message);
                throw;
            }

            return TwitterList;
        }




        public List<Twitter> TwitterTableDetailsfromSQL()
        {
           log.LogEvent("TwitterTableDetailsfromSQL function called ",DateTime.Now);
            try
            {

                // Call the LoadingCampingsDetails method to execute the SQL query
                // and pass the ReadbusinessOwnerDetailsFromDb method as a callback
                CampaingSql.LoadingCampingsDetails("select * from Twitter ", ReadCampaingsTableDetailsFromDb);

                // Return the dictionary of business owners
                return listTwitterTable;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                // Print the exception message if an error occurs
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public void ReadCampaingsTableDetailsFromDb(SqlDataReader reader)
        {
            log.LogEvent("ReadCampaingsTableDetailsFromDb function called ",DateTime.Now);
            try
            {
                // Clear the dictionary before inserting data from the database
                listTwitterTable.Clear();

                // Read each row in the data reader
                while (reader.Read())
                {
                    // Create a new businessOwner object
                    Twitter Twitter = new Twitter();


                    // Initialize the fields of the businessOwner object with data from the database
                    Twitter.userId = reader.GetInt32(0);


                    Twitter.associationName = reader.GetString(1);

                    Twitter.hashtag = reader.GetString(2);
                    Twitter.email = reader.GetString(3);
                    Twitter.userName = reader.GetString(4);
                    Twitter.twitterUsername = reader.GetString(5);
                    Twitter.CampaignName = reader.GetString(6);
                    Twitter.userMoney = reader.GetInt32(7);

                    ;
                    // Check if the dictionary already contains a business owner with the same userId

                    // Add the business owner object to the dictionary
                    listTwitterTable.Add(Twitter);

                }
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                throw;

            }


        }




        public void InsertUserSupportToDb(string associationName, string email, string userName, string hashtag, string CampaignName, string twitterUsername)
        {
            log.LogEvent("InsertnewSaleseToDb function called ",DateTime.Now);

            string insert = "INSERT INTO Twitter (associationName, hashtag, email, userName, twitterUsername, CampaignName, userMoney) VALUES (@associationName, @hashtag, @email, @userName, @twitterUsername, @CampaignName, @userMoney)";


            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        connection.Open();

                        //Add the user message data as parameters to the command

                        command.Parameters.AddWithValue("@associationName", associationName);
                        command.Parameters.AddWithValue("@hashtag", hashtag);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@userName", userName);
                        command.Parameters.AddWithValue("@twitterUsername", twitterUsername);
                        command.Parameters.AddWithValue("@CampaignName", CampaignName);
                        command.Parameters.AddWithValue("@userMoney", 0);

                        //Execute the command
                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (SqlException ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
              
                throw;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
