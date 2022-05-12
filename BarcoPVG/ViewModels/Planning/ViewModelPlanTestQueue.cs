using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BarcoPVG.Models;
using BarcoPVG.Models.Db;
using Microsoft.VisualBasic;

namespace BarcoPVG.ViewModels.Planning
{
    class ViewModelPlanTestQueue : AbstractViewModelBase
    {
        public ObservableCollection<PlPlanning> PlansToApprove { get; set; }
        protected PlPlanning _selectedPlan;

        //Constructor
        public ViewModelPlanTestQueue() : base()
        {
            Collection initialization;
            PlansToApprove = new ObservableCollection<PlPlanning>();

            foreach (var item in _daoPlanning.GetPlPlannings().Where(pl => pl.TestDivStatus == "In plan"))
            {
                PlansToApprove.Add(item);
            }

            // empty jr selected by default
            _selectedPlan = new PlPlanning();
        }

        // Getters/Setters
        public PlPlanning SelectedPlan
        {
            get => _selectedPlan;
            set
            {
                _selectedPlan = value;
                OnpropertyChanged();
            }
        }
    }
}
