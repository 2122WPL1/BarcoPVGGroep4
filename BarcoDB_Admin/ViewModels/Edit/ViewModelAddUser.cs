using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;
using System.Windows;

namespace BarcoDB_Admin.ViewModels.Edit
{
    public class ViewModelAddUser : AbstractViewModelContainer
    {

        public ViewModelAddUser() : base()
        {
            Person = new Person();

            BarcoDivisions = new BarcoDivision();
            TestDivision = new RqTestDevision();

            IsIntern = Visibility.Hidden;
        }



    }
}
