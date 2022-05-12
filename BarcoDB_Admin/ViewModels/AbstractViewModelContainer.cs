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
        // Jobrequest data container
        // Only one getter/setter needs to be made for all changes in GUI
        //protected JR _jr;
        //protected EUT _eut;
        protected Person _Person;
        protected PlResource _Resource;
        protected RqBarcoDivision _Division;


        // EUT's
        // Does not necessarily need to be linked to JR? We can retrieve the JR ID and add it in DAO
        //public ObservableCollection<EUT> EUTs { get; set; }
        //protected EUT _selectedEUT;

        public AbstractViewModelContainer()
        {
            //EUTs = new ObservableCollection<EUT>();
        }

        // Getters/Setters
        //public JR JR
        //{
        //    get { return _jr; }
        //    set
        //    {
        //        _jr = value;
        //        OnpropertyChanged();
        //    }
        //}
        //public EUT Eut
        //{
        //    get { return _eut; }
        //    set
        //    {
        //        _eut = value;
        //        OnpropertyChanged();
        //    }
        //}
        //public EUT SelectedEUT
        //{
        //    get { return _selectedEUT; }
        //    set
        //    {
        //        _selectedEUT = value;
        //        OnpropertyChanged();
        //    }
        //}

        public Person Person
        {
            get{ return _Person; }
            set { _Person = value; OnpropertyChanged(); }
        }

        public RqBarcoDivision Division
        {
            get { return _Division; }
            set { _Division = value; OnpropertyChanged(); }
        }

        public PlResource Resource
        {
            get { return _Resource; }
            set { _Resource = value; OnpropertyChanged(); }
        }

    }
}
