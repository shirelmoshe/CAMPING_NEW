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
    public class Donations:BaseEntitys
    {

        // Connection string for the database
        string connectionString = Environment.GetEnvironmentVariable("connectionString");

        // Method to create a new donation in the database
        public void CreateDonation(string CompanyName, string CampaignName, string Product, string Email, string Price, int Quantity)
        {

         
            // SQL insert statement to insert a new row into the Donation table
            string insert = "insert into Donation values (@CompanyName,@Product,@Email,@Price,@CampaignName,@Quantity)";

            try
            {
                // Use a SqlConnection and SqlCommand to execute the insert statement
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        connection.Open();

                        // Add the donation data as parameters to the command
                        command.Parameters.AddWithValue("@CompanyName", CompanyName);
                        command.Parameters.AddWithValue("@Product", Product);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@CampaignName", CampaignName);
                        command.Parameters.AddWithValue("@Quantity", Quantity);

                        // Execute the command
                        command.ExecuteNonQuery();
                      


                    }
                }
            }
            catch (SqlException ex)
            {
                log.LogError("Error occured while creating a new donation.", DateTime.Now);

                // Handle SQL exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                log.LogError( "Error occured while creating a new donation.",DateTime.Now);
                // Handle other exceptions
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // Dictionary to store the donations retrieved from the database
        Dictionary<int, Donation> dictionsryproducts = new Dictionary<int, Donation>();

        public Donations(Logger Log) : base(Log)
        {
        }



        // Method to retrieve all donations from the database and store them in the dictionary
        public Dictionary<int, Donation> ProductsDetailsFromSQL()
        {
          
            try
            {
                CampaingSql CampaingSql = new CampaingSql(base.log);

                // Call the LoadingCampingsDetails method of the campaingSql class to retrieve the donations
                CampaingSql.LoadingCampingsDetails("select * from  Donation", ReadCampingFromDb);

                // Return the dictionary of donations
                return dictionsryproducts;
            }
            catch (Exception ex)
            {
                log.LogError($"An error occured while trying to retrieve the donations information from the database: {ex.Message}", DateTime.Now);
                // Print the error message if an exception occurs
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        // Method to process each row returned by the query and create a Donation object to represent the donation
        public void ReadCampingFromDb(SqlDataReader reader)
        {
         
            // Clear the dictionary before inserting new information from the database
            dictionsryproducts.Clear();

            // Read each row and create a Donation object to represent the donation




            while (reader.Read())
            {
                Donation readProducts = new Donation();

                readProducts.productsId = reader.GetInt32(0);
                readProducts.CompanyName = reader.GetString(1);
                readProducts.Product = reader.GetString(2);
                readProducts.Email = reader.GetString(3);
                readProducts.Price = reader.GetString(4);
                readProducts.CampaignName = reader.GetString(5);
                //readProducts.Quantity = reader.GetInt32(6);







                //Cheking If Hashtable contains the key
                if (dictionsryproducts.ContainsKey(readProducts.productsId))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    dictionsryproducts.Add(readProducts.productsId, readProducts);
                }

            }


        }

        public Donation GetProductFromDbById(int id)
        {
        
            data.sql.DonationSql ProductFromSql = new data.sql.DonationSql(log);
            Donation productNew = (Donation)ProductFromSql.LoadOneProduct(id);
            return productNew;
        }

    }
}
