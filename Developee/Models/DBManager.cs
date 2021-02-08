using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Developee.Models
{
    public class DBManager
    {
        public static bool ValidateUser(string username, string password)
        {
            bool status = false;

            IDbConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["dbconnectionString"].ConnectionString;
            IDbCommand cmd = new SqlCommand();
            cmd.Connection = con;
            string query = "select * from Customer where username='" + username + "' and password='" + password + "'";
            cmd.CommandText = query;
            try
            {
                con.Open();
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    status = true;
                }
            }
            catch (Exception exp)
            {
                string expMsg = exp.Message;
                Console.WriteLine(expMsg);
            }

            finally
            {
                con.Close();
            }
            return status;
        }
    }
}