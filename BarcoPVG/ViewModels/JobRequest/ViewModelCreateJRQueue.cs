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

namespace BarcoPVG.Viewmodels.JobRequest
{
    public class ViewModelCreateJRQueue : AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelCreateJRQueue() : base()
        {
            Load();
            ChangeColorJobNature();
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _dao.GetAllJobRequests().Where(rq => rq.Requester == _dao.BarcoUser.Name);
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds) // Change Date +5, Change Job nature to colors
            {
                //requestId.ExpectedEnddate = requestId.RequestDate.AddDays(5.0);
                IdRequestsOnly.Add(requestId);

                
            }
        }

        public void ChangeColorJobNature()
        {
            if (true)
            {
                //als job nature qualification is → kleur groen
                //else if job nature ... is → kleur rood
                //else kleur grijs
            }   
        }
    }
}
