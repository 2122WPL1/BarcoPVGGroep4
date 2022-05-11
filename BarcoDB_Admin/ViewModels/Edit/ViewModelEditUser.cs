using BarcoDB_Admin.ViewModels;
using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcoDB_Admin.Dao;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
     class ViewModelEditUser : AbstractViewModelBase
    {
        private Person _Person;

        public Person SelectedUser { get; set; }
        public ViewModelEditUser(string Afkorting) : base()
        {
            //SelectedUser = user;
            //_Person = _daoUser.GetUser(Afkorting);
        }

        public Person Person
        { 
            get { return _Person; } 
            set { _Person = value; OnpropertyChanged(); }
        }
    }
}
