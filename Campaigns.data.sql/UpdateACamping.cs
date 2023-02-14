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
    public class UpdateACamping : BaseSql
    {
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        public UpdateACamping(Logger Log) : base(Log)
        {
        }

        public void UpdateCamping(int userId, string associationName, string email, string uri, string hashtag, string CampaignName)
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
