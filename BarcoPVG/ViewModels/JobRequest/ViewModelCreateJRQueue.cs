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

namespace BarcoPVG.ViewModels.JobRequest
{
    public class ViewModelCreateJRQueue : AbstractViewModelCollectionRQ
    {
        public Visibility NewInternJR { get; set; }

        //Constructor
        public ViewModelCreateJRQueue() : base()
        {
            Init();
            Load();
        }

        private void Init()
        {
            if (_dao.BarcoUser.Function == "TEST")
            {
                NewInternJR = Visibility.Visible;
            }
            else
            {
                NewInternJR = Visibility.Hidden;
            }
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _daoJR.GetAllJobRequests().Where(rq => rq.Requester == _dao.BarcoUser.Name);
            IdRequestsOnly.Clear();



            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
