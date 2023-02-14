using System;
using System.Collections.Generic;
using Campaigns.Model;
using System.Data.SqlClient;
using Campaigns.data.sql;
using System.Configuration;
using Utilitis;


namespace Campaigns.Entities
{
    public class Campaigns:BaseEntitys

    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");
        CampaingSql CampaingSql;
        public Campaigns(Logger Log) : base(Log)
        {
            CampaingSql = new CampaingSql(base.log);
        }
        //  private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        Dictionary<int, Campaign> dictionsryCampaing = new Dictionary<int, Campaign>();
       
        public void InsertUserMessageToDb(string associationName, string email, string uri, string hashtag, string CampaignName)
        {
           log.LogEvent("InsertUserMessageToDb function called "  ,DateTime.Now);

            string insert = "insert into  Campaigns values (@associationName,@email,@uri,@hashtag,@CampaignName)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        connection.Open();

                        //Add the user message data as parameters to the command

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
                log.LogError("SQL exception occurred: {0}", DateTime.Now);
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                log.LogError( "Exception occurred: {0}",DateTime.Now);
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void ReadCampingFromDb(SqlDataReader reader)
        {

            //Clear Dictionary Before Inserting Information From Sql Server
            dictionsryCampaing.Clear();


            while (reader.Read())
            {
                Campaign readCampaing = new Campaign();

                readCampaing.userId = reader.GetInt32(0);
                readCampaing.associationName = reader.GetString(1);
                readCampaing.email = reader.GetString(2);
                readCampaing.uri = reader.GetString(3);
                readCampaing.hashtag = reader.GetString(4);


                //Cheking If Hashtable contains the key
                if (dictionsryCampaing.ContainsKey(readCampaing.userId))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    dictionsryCampaing.Add(readCampaing.userId, readCampaing);
                }

            }


        }

        public Dictionary<int, Campaign> CampaignDetailsFromSQL()
        {
            try
            {
                //Dictionary<int, Campaign> ret;

                CampaingSql.LoadingCampingsDetails("select * from Campaigns", ReadCampingFromDb);

                return dictionsryCampaing;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);

                Console.WriteLine(ex.Message);
                return null;
                throw;

            }



        }

        Dictionary<int, Campaign> dictionsryCampaingTable = new Dictionary<int, Campaign>();

        public Dictionary<int, Campaign> campaingsTableDetailsfromSQL()
        {
          //  logger.Info("campaingsTableDetailsfromSQL function called  ");
            try
            {
                // Call the LoadingCampingsDetails method to execute the SQL query
                // and pass the ReadbusinessOwnerDetailsFromDb method as a callback
               // CampaingSql.LoadingCampingsDetails("select * from Campaigns", ReadCampaingsTableDetailsFromDb);

                // Return the dictionary of business owners
                return dictionsryCampaingTable;
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);
                // Print the exception message if an error occurs
                Console.WriteLine(ex.Message);
                return null;
                throw;
            }
        }


        public void ReadCampaingsTableDetailsFromDb(SqlDataReader reader)
        {
           log.LogEvent("ReadCampaingsTableDetailsFromDb function called  ",DateTime.Now);
            try
            {
                // Clear the dictionary before inserting data from the database
                dictionsryCampaingTable.Clear();

                // Read each row in the data reader
                while (reader.Read())
                {
                    // Create a new businessOwner object
                    Campaign readDictionsryCampingDetails = new Campaign();

                    // Initialize the fields of the businessOwner object with data from the database
                    readDictionsryCampingDetails.userId = reader.GetInt32(0);
                    readDictionsryCampingDetails.associationName = reader.GetString(1);
                    readDictionsryCampingDetails.email = reader.GetString(2);
                    readDictionsryCampingDetails.uri = reader.GetString(3);
                    readDictionsryCampingDetails.hashtag = reader.GetString(4);
                    readDictionsryCampingDetails.CampaignName = reader.GetString(5);
                    ;
                    // Check if the dictionary already contains a business owner with the same userId
                    if (dictionsryCampaingTable.ContainsKey(readDictionsryCampingDetails.userId))
                    {
                        // Key already exists, log a warning message
                        Console.WriteLine("Warning: Dictionary already contains business owner with userId: " + readDictionsryCampingDetails.userId);
                    }
                    else
                    {
                        // Add the business owner object to the dictionary
                        dictionsryCampaingTable.Add(readDictionsryCampingDetails.userId, readDictionsryCampingDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogException("Exception occurred: {0}", ex, DateTime.Now);

                throw;


            }
        }


        public List<Campaign> GetShippingFromDbById(string id)
        {

            data.sql.CampaingSql userFromSql = new data.sql.CampaingSql(log);
            List<Campaign> Campaign = userFromSql.LoadCampaign(id);
            return Campaign;
        }

        public void UpdateCampingInDb(string userId, string associationName, string email, string uri, string hashtag, string CampaignName)
        {
            data.sql.UpdateACamping camping = new data.sql.UpdateACamping(log);

            camping.UpdateCamping(int.Parse(userId), associationName, email, uri, hashtag, CampaignName);
        }


        public void DeleteAProductByCampingID(int userId)
        {
            data.sql.CampaingSql camping = new data.sql.CampaingSql(log);
            camping.DeleteCampaig(userId);
        }
    }
}
