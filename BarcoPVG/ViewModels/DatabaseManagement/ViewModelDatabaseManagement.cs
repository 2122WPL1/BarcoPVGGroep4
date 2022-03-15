using BarcoPVG.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarcoPVG.ViewModels.DatabaseManagement
{
    public class ViewModelDatabaseManagement : AbstractViewModelBase //AbstractViewModelCollectionRQ
    {

        public ICommand AddNewUserCommand { get; set; }
        public ICommand DeleteUserCommand { get; set;}
        public ICommand AddNewResourceCommand { get; set; }
        public ICommand DeleteResourceCommand { get; set; }
        public ICommand ReturnMainWindowCommand { get; set; }

        public ViewModelDatabaseManagement()
        {

        }
        //public ViewModelDatabaseManagement() : base()
        //{
        //    Load();
        //}

        //public void Load()
        //{
        //    var requestIds = _dao.GetAllJobRequests();
        //    IdRequestsOnly.Clear();

        //    foreach (var requestId in requestIds)
        //    {
        //        IdRequestsOnly.Add(requestId);
        //    }
        //}
    }
}
