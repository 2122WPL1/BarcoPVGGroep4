﻿using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using BarcoPVG.Models.Classes;

namespace BarcoPVG.ViewModels.TestGUI
{
    class ViewModelUpdateTestQueue : AbstractViewModelBase
    {
        public ObservableCollection<Test> Tests { get; set; }
        public ObservableCollection<string> StatusList { get; set; }

        private Test selectedTest;

        public ICommand SaveStatusChangesCommand { get; set; }

        public ViewModelUpdateTestQueue()
        {
            StatusList = new ObservableCollection<string> 
            { 
                "Planned",
                "Tested",
                "Tested but fail",
                "Delay (requester)",
                "Delay (PVG)",
                "Finished - passed",
                "Finished - failed",
                "Reported"
            };

            Tests = new ObservableCollection<Test>();

            foreach (var item in _daoPlanning.GetAllTests().Where(t => t.Status != "Unconfirmed"))
            {
                Tests.Add(item);
            }
                
            SaveStatusChangesCommand = new DelegateCommand(SaveStatusChanges);

            selectedTest = new Test();
        }
        public Test SelectedTest 
        { 
            get => selectedTest;
            set
            {
                selectedTest = value;
                OnpropertyChanged();
            }
        }

        public void SaveStatusChanges()
        {
            foreach (var item in Tests)
            {
                _daoPlanning.UpdateTestStatus(item);
            }
        }
    }
}
