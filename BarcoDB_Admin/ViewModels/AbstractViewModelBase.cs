using BarcoDB_Admin.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;



namespace BarcoDB_Admin.ViewModels
{
    public abstract class AbstractViewModelBase : INotifyPropertyChanged
    {
        //Jarne
        protected DaoUser _daoUser = DaoUser.InstanceUser();
        protected DaoResource _daoResource = DaoResource.InstanceResource();

        protected DAO _dao = DAO.Instance();
        //protected BarcoDB_Admin.Dao.DAO _dao = BarcoDB_Admin.Dao.DAO.Instance(); //dao's apart in de viewmodels gedefineerd

        // Constructor
        public AbstractViewModelBase()
        {
            
        }

        // Implement propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnpropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}