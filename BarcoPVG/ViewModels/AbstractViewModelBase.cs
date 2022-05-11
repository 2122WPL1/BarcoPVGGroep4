using BarcoPVG.Dao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;


namespace BarcoPVG.ViewModels
{
    public abstract class AbstractViewModelBase : INotifyPropertyChanged
    {
        protected DAO _dao = DAO.Instance();
        protected DaoJR _daoJR = new DaoJR();
        protected DaoApproval _daoApproval = new DaoApproval();
        protected DaoPlanning _daoPlanning = new DaoPlanning();
        protected DaoResources _daoResources = new DaoResources();
        protected DaoPerson _daoPerson = new DaoPerson();

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
