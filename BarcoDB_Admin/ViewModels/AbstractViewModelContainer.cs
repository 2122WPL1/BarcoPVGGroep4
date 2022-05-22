using BarcoDB_Admin.Models.Classes;
using BarcoDB_Admin.Models.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace BarcoDB_Admin.ViewModels
{
    public abstract class AbstractViewModelContainer : AbstractViewModelBase
    {
        protected Person _Person;
        protected PlResource _Resource;
        protected RqBarcoDivision _Division;
        protected RqTestDevision _TestDivision;
        protected BarcoDivision _BarcoDivisions;


        Visibility _IsIntern; 


        public AbstractViewModelContainer()
        {

        }

        public Person Person
        {
            get{ return _Person; }
            set 
            { 
                _Person = value; 
                OnpropertyChanged();

                if (_Person.Functie == "TEST")
                {
                    _IsIntern = Visibility.Visible;
                }
                else
                {
                    _IsIntern &= Visibility.Hidden;
                }
            }
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

        public RqTestDevision TestDivision
        {
            get
            {
                return _TestDivision;
            }
            set
            {
                _TestDivision = value;
                OnpropertyChanged();
            }
        }

        public PlResource Resource
        {
            get { return _Resource; }
            set { _Resource = value; OnpropertyChanged(); }
        }

        public BarcoDivision BarcoDivisions
        {
            get { return _BarcoDivisions; }
            set { _BarcoDivisions = value; OnpropertyChanged(); }
        }


        public Visibility IsIntern 
        { 
            get => _IsIntern; 
            set
            {
                _IsIntern = value; 
                OnpropertyChanged();
            }
        }     
    }
}
