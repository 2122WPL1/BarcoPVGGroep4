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
        protected Person _Person;
        protected PlResource _Resource;
        protected RqBarcoDivision _Division;


        public AbstractViewModelContainer()
        {
            //EUTs = new ObservableCollection<EUT>();
        }

        public Person Person
        {
            get{ return _Person; }
            set { _Person = value; OnpropertyChanged(); }
        }

        public RqBarcoDivision Division
        {
            get 
            { 
                return _Division; 
            }
            set 
            { 
                _Division = value;
                OnpropertyChanged(); 
            }
        }

        public PlResource Resource
        {
            get { return _Resource; }
            set { _Resource = value; OnpropertyChanged(); }
        }

    }
}
