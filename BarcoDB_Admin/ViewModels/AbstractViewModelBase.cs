using BarcoDB_Admin.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;



namespace BarcoDB_Admin.Viewmodels
{
    public abstract class AbstractViewModelBase : INotifyPropertyChanged
    {
        //protected DAO _dao = DAO.Instance();
        //protected BarcoDB_Admin.Dao.DAO _dao = BarcoDB_Admin.Dao.DAO.Instance(); //dao's apart in de viewmodels gedefineerd

        // Constructor
        public AbstractViewModelBase()
        {
            
        }

        // Implement propertyChanged
        // Start boilerplate code
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnpropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }


        // End boilerplate code
    }
}