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
        protected DaoJR _daoJR = DaoJR.InstanceJR();
        protected DaoApproval _daoApproval = DaoApproval.InstanceApproval();
        protected DaoEUT _daoEUT = DaoEUT.InstanceEUT();
        protected DaoPlanning _daoPlanning = DaoPlanning.InstancePlanning();
        protected DaoResources _daoResources = DaoResources.InstanceResources();
        protected DaoPerson _daoPerson = DaoPerson.InstancePerson();
        protected DaoLogin _daoLogin = DaoLogin.InstanceLogin();
        protected DaoInternalJR _daoInternalJr = DaoInternalJR.InstanceInternalJR();

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
