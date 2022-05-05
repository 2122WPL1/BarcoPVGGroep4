using BarcoDB_Admin.ViewModels;
using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.ViewModels.Edit
{
     class ViewModelEditUser : AbstractViewModelBase
    {
        public Person SelectedUser { get; set; }
        public ViewModelEditUser(Person user) : base()
        {
            SelectedUser = user;
        }

        //public Person SelectedUser
        //{
        //    get => _SelectedUser;
        //    set
        //    {
        //        _SelectedUser = value;
        //        OnpropertyChanged();
        //    }
        //}
    }
}
