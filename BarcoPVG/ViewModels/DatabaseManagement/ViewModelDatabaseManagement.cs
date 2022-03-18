using BarcoPVG.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BarcoPVG.ViewModels.DatabaseManagement
{
    public class ViewModelDatabaseManagement : AbstractViewModelCollectionRQ
    {
        public ViewModelDatabaseManagement() : base()
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
