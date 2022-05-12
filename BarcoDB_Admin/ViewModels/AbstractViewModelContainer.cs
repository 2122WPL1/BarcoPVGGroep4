using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BarcoDB_Admin.ViewModels
{
    public abstract class AbstractViewModelContainer : AbstractViewModelBase
    {
        public Person _Person;

        public AbstractViewModelContainer()
        {

        }
        public Person Person
        {
            get { return _Person; }
            set { _Person = value; OnpropertyChanged(); }
        }

    }
}
