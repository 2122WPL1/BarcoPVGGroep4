using BarcoPVG.Models.Db;
using BarcoPVG.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarcoPVG.ViewModels.DatabaseManagement
{
    public class ViewModelDBUser : AbstractViewModelCollectionRQ
    {
        public ObservableCollection<Person> AllUsers { get; set; }
        public ViewModelDBUser() : base()
        {
            Load();
        }

        //Amy
        private void Load()
        {
            //List<Person> allUser = _dao.GetAllUser();
            //var getUsers = _dao.GetAllUser();
            AllUsers = new ObservableCollection<Person>();

            foreach (var getUser in _dao.GetAllUser())
            {
                AllUsers.Add(getUser);
            }
        }
    }
}
