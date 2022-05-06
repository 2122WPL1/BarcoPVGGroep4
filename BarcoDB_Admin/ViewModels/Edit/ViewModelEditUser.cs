using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this._Person = _dao.GetUser(Afkorting);
        }

        public Person Person
        { 
            get { return _Person; } 
            set { _Person = value; OnpropertyChanged(); }
        }
    }
}
