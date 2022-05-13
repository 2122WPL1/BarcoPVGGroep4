﻿using Microsoft.Toolkit.Mvvm.Input;
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
        public Visibility NoExternForTestTeam { get; set; }

        //Constructor
        public ViewModelCreateJRQueue() : base()
        {
            Init();
            Load();
        }

        private void Init()
        {
            if (_daoLogin.BarcoUser.Functie == "TEST" || _daoLogin.BarcoUser.Functie == "DEV")
            {
                NewInternJR = Visibility.Visible;
                NoExternForTestTeam = Visibility.Hidden;
            }
            else
            {
                NewInternJR = Visibility.Hidden;
                NoExternForTestTeam = Visibility.Visible;
            }
        }

        // Functie used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _daoJR.GetAllJobRequests().Where(rq => (rq.Requester == _daoLogin.BarcoUser.Name)/* && (rq.JrStatus == "To approve")*/);
            IdRequestsOnly.Clear();



            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
