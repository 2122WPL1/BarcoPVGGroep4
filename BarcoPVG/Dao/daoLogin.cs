using System.Collections.Generic;
using System.Linq;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Dao
{
    public class DaoLogin : DAO
    {
        //Jarne
        //here comes all the data from Login

        //kopieert de context van DAO
        public DaoLogin() : base()
        {
                
        }
        //Eakarach
        //Login
        public void LoginSucceeded(Person loginPerson)
        {           
            //Put division or to list if they have more than one division
            //string division = GetAllDivisions().Where(div => "TS" == loginPerson.Afkorting).ToString();

            //Put Function to give right the the user
            //string func = "";
            this.BarcoUser = new BarcoUser()
            {
                Name = loginPerson.Voornaam,
                Division = GetAllDivForPerson(loginPerson)[0].Pvggroup,
                Function = "DEV",
            };
        }

        public List<RqBarcoDivisionPerson> GetAllDivForPerson(Person loginperson)
        {
            List<RqBarcoDivisionPerson> output = new List<RqBarcoDivisionPerson>();
            string text = null;

            List<RqBarcoDivisionPerson> list = _context.RqBarcoDivisionPeople.Where(p => p.AfkPerson == loginperson.Afkorting).ToList();

            foreach (RqBarcoDivisionPerson result in list)
            {
                text = result.Pvggroup;

                if (output.FirstOrDefault(x => x.Pvggroup == text) == null)
                {
                    output.Add(result);
                }
            }

            return output;
        }

    }
}
