using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoPVG.Dao
{
    public static class LogIn
    {
        public static string LoginSucceedded()
        {
            string msg = null;
            try
            {
                string query = "SELECT COUNT(1) FROM PEOPLE"
                    + " Voornaam=@username";
            }
            catch (Exception ex)
            {

                throw;
            }
            return msg;
        }
    }
}
