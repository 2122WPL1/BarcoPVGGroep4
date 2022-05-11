using BarcoDB_Admin.Models.Db;
using BarcoDB_Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.ViewModels.Edit
{
    public class ViewModelAddUser : AbstractViewModelBase
    {
        protected Person _Person;

        public ViewModelAddUser() : base()
        {
        }

        public Person Person
        {
            get { return _Person; }
            set { _Person = value; OnpropertyChanged(); }
        }
    }
}
