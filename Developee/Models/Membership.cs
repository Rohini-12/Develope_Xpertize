using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Developee.Models
{
    public class Membership
    {

        public static bool ValidateUser(string userName, string password)
        {
            bool status = false;
            Console.WriteLine(userName + " " + password);
            //validation logic 
            if (DBManager.ValidateUser(userName, password))
            {
                status = true;
            }
            return status;
        }
    }
}