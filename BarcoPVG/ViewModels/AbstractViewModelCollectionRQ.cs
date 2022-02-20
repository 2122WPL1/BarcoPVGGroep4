using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BarcoPVG.Models;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Viewmodels
{
    public abstract class AbstractViewModelCollectionRQ : AbstractViewModelBase
    {
        public ObservableCollection<RqRequest> IdRequestsOnly { get; set; }
        protected RqRequest _selectedJR;

        //Constructor
        public AbstractViewModelCollectionRQ() : base()
        {
            // Collection initialization
            IdRequestsOnly = new ObservableCollection<RqRequest>();

            // empty jr selected by default
            _selectedJR = new RqRequest();
        }

        // Getters/Setters
        public RqRequest SelectedJR
        {
            get => _selectedJR;
            set
            {
                _selectedJR = value;
                OnpropertyChanged();
            }
        }
    }
}
