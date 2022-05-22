using BarcoDB_Admin.Dao;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace BarcoDB_Admin.ViewModels
{
    public abstract class AbstractViewModelBase : INotifyPropertyChanged
    {
        //Jarne
        protected DaoUser _daoUser = DaoUser.InstanceUser();
        protected DaoResource _daoResource = DaoResource.InstanceResource();
        protected DaoDivision _daoDivision = DaoDivision.InstanceDivision();

        protected DAO _dao = DAO.Instance();
        //protected BarcoDB_Admin.Dao.DAO _dao = BarcoDB_Admin.Dao.DAO.Instance(); //dao's seperatly defined in viewmodels

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