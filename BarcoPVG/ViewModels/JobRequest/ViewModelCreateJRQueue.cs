using Microsoft.Toolkit.Mvvm.Input;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BarcoPVG;
using BarcoPVG.Views.JobRequest;
using System.Windows.Media;
using System.Windows.Data;

namespace BarcoPVG.Viewmodels.JobRequest
{
    public class ViewModelCreateJRQueue : AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelCreateJRQueue() : base()
        {
            Load();   
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _dao.GetAllJobRequests().Where(rq => rq.Requester == _dao.BarcoUser.Name);
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }



    }
}
