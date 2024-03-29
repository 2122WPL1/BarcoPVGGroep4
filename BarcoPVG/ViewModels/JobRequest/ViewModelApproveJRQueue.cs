﻿using BarcoPVG.Models.Db;
using System.Collections.Generic;
using System.Linq;

namespace BarcoPVG.ViewModels.JobRequest
{
    class ViewModelApproveJRQueue: AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelApproveJRQueue() : base()
        {
            Load();
        }

        // Functie used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            // Get unapproved JR's
            //Eakarach
            // Show all JR that not yet be approved
            List<RqRequest> requestIds = _daoJR.GetAllJobRequests().Where(rq => rq.JrStatus == "To approve").ToList();
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
