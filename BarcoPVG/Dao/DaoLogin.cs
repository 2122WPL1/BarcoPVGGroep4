using System;
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
        protected DaoPerson _daoPerson = new();
        protected static readonly DaoLogin _instanceLogin = new();
        public BarcoUser BarcoUser { get; set; }
        public static DaoLogin InstanceLogin()
        {
            return _instanceLogin;
        }

        ///copies the data from DAO
        public DaoLogin() : base()
        {

        }

        //Eakarach
        //Login
        public void LoginSucceeded(Person loginPerson)
        {
            //Put division or to list if they have more than one division
            string? division = null;

            BarcoUser = new BarcoUser();
            BarcoUser.Name = loginPerson.Voornaam;

            if (GetAllDivForPerson(loginPerson).Count > 0)
            {
                division = GetAllDivForPerson(loginPerson).FirstOrDefault(x => x.AfkPerson == loginPerson.Afkorting).Pvggroup != null ? division : "";
                BarcoUser.Division = division;
            }
            else
            {
                BarcoUser.Division = "Extern";
            }
            //Put Function to give right the the user
            //string func = "";

            //Jarne getting the info from the login details to get the right view display
            
            
            BarcoUser.Function = GetFuntion(division);

            //BarcoUser.Function = "DEV"; log in as Developer


        }

        //Eakarach
        private string GetFuntion(string? division)
        {
            switch (division)
            {
                case null:
                    return ""; // Log in als extern
                default:
                    return "TEST"; // Log in als TEST team
                    break;
            }

            return null;
        }

        public List<RqBarcoDivisionPerson> GetAllDivForPerson(Person loginperson)
        {
            List<RqBarcoDivisionPerson> output = new List<RqBarcoDivisionPerson>();
            string text = null;

            List<RqBarcoDivisionPerson> list = _context.RqBarcoDivisionPeople.Where(p => p.AfkPerson == loginperson.Afkorting).ToList();

            foreach (RqBarcoDivisionPerson result in list)
            {
                //if (GetAllDivForPerson().FirstOrDefault(x => x.Afkorting == result.Pvggroup))
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