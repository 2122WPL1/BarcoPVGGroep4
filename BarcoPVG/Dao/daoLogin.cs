using System.Collections.Generic;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoLogin : DAO
    {
        //Jarne
        //here comes all the data from Login
        protected DaoPerson _daoPerson = new();

        ///copies the data from DAO
        public DaoLogin() : base()
        {

        }

        //Eakarach
        //Login
        public void LoginSucceeded(Person loginPerson)
        {
            string name = loginPerson.Voornaam;
            
            //Put division or to list if they have more than one division
            //string division = GetAllDivisions().Where(div => "TS" == loginPerson.Afkorting).ToString();

            //Put Function to give right the the user
            //string func = "";
            this.BarcoUser = new BarcoUser()
            {
                Name = name,
                Division = "DEV",
                Function = "DEV",
            };
        }

        public void GetDiv(Person loginPerson)
        {
            List<RqBarcoDivision> listDiv = _daoPerson.GetAllDivisions();
            foreach (RqBarcoDivision div in listDiv)
            {
                //if (div.Afkorting == loginPerson)
                //{
                    
                //}
            }
        }
    }
}
