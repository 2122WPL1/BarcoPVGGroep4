using BarcoDB_Admin.Models.Db;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BarcoDB_Admin.Models.Classes;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy,Eakarach
    class ViewModelEditUser : AbstractViewModelContainer
    {
        ObservableCollection<Person> personDetails { get; set; }
        public bool _IsEnable1;
        public bool IsEnable1 //ensure that primary can not be changed
        {
            get
            {
                return _IsEnable1;
            }
            set
            {
                _IsEnable1 = value;
                OnpropertyChanged();
            }
        }
        public ViewModelEditUser(string Afkorting) : base()
        {
            //Prevent Null exception
            Person = new Person();
            BarcoDivisions = new BarcoDivision();
            TestDivision = new RqTestDevision();




            Person = _daoUser.GetAllUser().FirstOrDefault(x => x.Afkorting == Afkorting); //to be able to edit the selected person in the database

            if (Person.Functie == "TEST")
            {
                //Find Testeam
                var afkortingTest = _daoUser.GetAllBarcoDivisionPersons()
                    .FirstOrDefault(t => t.AfkPerson == Person.Afkorting);
                TestDivision = _daoUser.GetAllTestTeam().FirstOrDefault(t => t.Afkorting == afkortingTest.Pvggroup);

                //Find Division
                var afkortingDiv = _daoUser.GetAllBarcoDivisionPersons().FirstOrDefault(dp => dp.AfkPerson == Person.Afkorting);
                Division = _daoDivision.GetAllDivisions().FirstOrDefault(d => d.Afkorting == afkortingDiv.AfkDevision);
            }
            

            IsEnable1 = false;

            
                 
            
        }
    }
}
