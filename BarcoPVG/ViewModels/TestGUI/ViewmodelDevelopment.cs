using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Serialization;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BarcoPVG.Dao;
using BarcoPVG.Models;
using BarcoPVG.Views;

namespace BarcoPVG.ViewModels.TestGUI
{
    // TEMPORARY SCREEN
    // Proof of concept: loading list of JR's from database
    // TODO: datatemplate for JR's
    public class ViewModelDevelopment : AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelDevelopment():base()
        {
            Load();
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _daoJR.GetAllJobRequests();
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
