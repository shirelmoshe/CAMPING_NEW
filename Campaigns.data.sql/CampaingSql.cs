using Campaigns.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using Utilitis;

namespace Campaigns.data.sql
{
    public class CampaingSql:BaseSql
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public delegate void SetDataReader_delegate1(SqlDataReader reader);

        public CampaingSql(Logger Log) : base(Log) { }
        
       

        // private static readonly Logger logger = LogManager.GetCurrentClassLogger();





        public Dictionary<int, Campaign> AddCampaignToDictionary(SqlDataReader reader)
        {
            log.LogEvent("AddCampaignToDictionary function called with SqlDataReader",DateTime.Now);

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
        public void LoadingCampingsDetails(string SqlQuery, SetDataReader_delegate1 Ptrfunc)
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
              
                // log the error message

                throw;
            }
            catch (Exception ex)
            {



                // log the error message
              
                throw;
            }
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
                log.LogException( "SQL exception occurred", ex,DateTime.Now);
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
                log.LogException("SQL exception occurred", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                log.LogException("SQL exception occurred", ex, DateTime.Now);
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
