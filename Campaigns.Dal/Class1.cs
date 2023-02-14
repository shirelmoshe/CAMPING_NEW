using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Campaigns.Model;
using System.Data.SqlClient;

namespace Campaigns.Dal
{
    public class SqlQuery

    {

        string connectionString = "Integrated Security = SSPI; Persist Security Info=False;Initial Catalog = campaign ; Data Source = localhost\\sqlEXPRESS";

        //delegate for passing a function to execute on a SqlDataReader
        public delegate void pustDataReader_delegate(Campaign userComment, SqlCommand command);
        public delegate void pustDataReader_delegate1(Donation donorDetails, SqlCommand command);
        public delegate void SetDataReader_delegate(SqlDataReader reader);
        public delegate void SetDataReader_delegate1(SqlDataReader reader);
        public delegate object SetResulrDataReader_delegate(SqlDataReader reader);



        // ...




        SqlConnection connection;
        public SqlQuery()
        {
            //initialize the connection string
            connection = new SqlConnection(connectionString);
        }


        public bool Connect()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                // Log the error message and stack trace
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                // Return false to indicate that the connection failed
                return false;
            }
        }




        //run the sql command and pass a function to execute on the S

        public static void RunCommand(string sqlQuery, SetDataReader_delegate func)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string queryString = sqlQuery;

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        func(reader);
                    }
                }

            }

        }

        public static object RunCommandResult(string sqlQuery, SetResulrDataReader_delegate func)
        {
            object ret = null;
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                string queryString = sqlQuery;

                // Adapter
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Call the delegate function and pass in the reader
                        ret = func(reader);
                    }
                }

            }

            // Return the result
            return ret;
        }


        // Function to add new user comment to the database
        public void RunAddUserComment(string sqlQuerey, pustDataReader_delegate func, Campaign userComment)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(userComment, command);

            }

        }


        // Function to add new user donation to the database
        public void RunAddUserDonation(string sqlQuerey, pustDataReader_delegate1 func, Donation donorDetails)
        {
            if (!Connect()) return;
            string insert = sqlQuerey;
            ;
            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                func(donorDetails, command);

            }
        }

        public void runCommand1(string sqlQuery, SetDataReader_delegate func)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Adapter
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    //Reader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        func(reader);
                    }
                }
            }

        }




    }
}