using BarcoPVG.Models.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BarcoPVG.ViewModels
{
    public abstract class AbstractViewModelContainer : AbstractViewModelBase
    {
        // Jobrequest data container
        // Only one getter/setter needs to be made for all changes in GUI
        protected JR _jr;
        protected EUT _eut;

        // EUT's
        // Does not necessarily need to be linked to JR? We can retrieve the JR ID and add it in DAO

        private ObservableCollection<EUT> _EUTs;

        public ObservableCollection<EUT> EUTs
        {
            get { return _EUTs; }
            set
            {
                _EUTs = value;
                OnpropertyChanged();
            }
        }

        public EUT _selectedEUT;

        public AbstractViewModelContainer()
        {
            EUTs = new ObservableCollection<EUT>();
        }

        // Getters/Setters
        public JR JR
        {
            get { return _jr; }
            set
            {
                _jr = value;
                OnpropertyChanged();
            }
        }
        public EUT Eut
        {
            get { return _eut; }
            set
            {
                _eut = value;
                OnpropertyChanged();
            }
        }
        public EUT SelectedEUT
        {
            get { return _selectedEUT; }
            set
            {
                _selectedEUT = value;
                OnpropertyChanged();
            }
        }
    }
}
