using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BarcoPVG.Models.Classes;
using BarcoPVG.Models;
using BarcoPVG.Models.Db;

namespace BarcoPVG.Viewmodels.Planning
{
    // Kaat
    class ViewModelPlanTestForm : AbstractViewModelBase
    {
        // Listbox with equipment
        public ObservableCollection<PlResource> Resources { get; set; }

        // Plan for these tests
        public PlPlanning SelectedPlan { get; set; }

        public ObservableCollection<Test> Tests { get; set; }
        private Visibility doubleBooked;
        private Test selectedTest;
        private Test editingTest; 

        // Used to triigger check for dates
        private DateTime? startDate;
        private DateTime? endDate;

        public ICommand AddNewTestCommand { get; set; }
        public ICommand ClearTestCommand { get; set; }
        public ICommand DeleteTestCommand { get; set; }

        public ViewModelPlanTestForm(PlPlanning planning)
        {
            SelectedPlan = planning;

            Resources = new ObservableCollection<PlResource>();
            Tests = new ObservableCollection<Test>();

            foreach (var item in _dao.GetResources(planning.TestDiv))
            {
                Resources.Add(item);
            }

            foreach (var item in _dao.GetTestsForJRAndDivision(SelectedPlan.IdRequest, SelectedPlan.TestDiv))
            {
                Tests.Add(item);
            }

            AddNewTestCommand = new DelegateCommand(AddTest);
            ClearTestCommand = new DelegateCommand(ClearTest);
            DeleteTestCommand = new DelegateCommand(DeleteTest);

            doubleBooked = Visibility.Hidden;
            ClearTest();
        }


        public Test SelectedTest
        {
            get => selectedTest;
            set
            {
                selectedTest = value;
                EditingTest = selectedTest;
                OnpropertyChanged();
            }
        }

        public Test EditingTest
        {
            get => editingTest;
            set
            {
                editingTest = value;
                StartDate = editingTest is null? null :  editingTest.StartDate;
                EndDate = editingTest is null ? null : editingTest.EndDate;
                OnpropertyChanged();
            }
        }

        public DateTime? StartDate 
        { 
            get => startDate;
            set
            {              
                startDate = value;
                if(editingTest == null)
                {
                    editingTest = new Test(); //Sander: wanneer een test verwijderd wordt dan bestaad editingTest niet meer dus geef ik hem hier een lege Test
                }
                editingTest.StartDate = value; 

                SetVisibility();
                OnpropertyChanged();
            }
        }

        public DateTime? EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                editingTest.EndDate = value;
                SetVisibility();
                OnpropertyChanged();
            }
        }

        public Visibility DoubleBooked 
        { 
            get => doubleBooked;
            set
            {
                doubleBooked = value;
                OnpropertyChanged();
            }
        }

        public void AddTest() //Sander: controle op de input
        {
            if (editingTest.Resource == null)
            {
                MessageBox.Show("Select a Resource");
                return;
            }
            if (startDate >= endDate)
            {
                MessageBox.Show("End Date Can't be before or the same as Start Date");
                return;
            }
            if(startDate == null || endDate == null)
            {
                MessageBox.Show("End Date and Start Date must be selected");
                return;
            }
           

            Test newTest = new Test
            {
                Description = EditingTest.Description,
                RQId = SelectedPlan.IdRequest,
                TestDivision = SelectedPlan.TestDiv,
                StartDate = editingTest.StartDate,
                EndDate = editingTest.EndDate,
                Resource = editingTest.Resource,
                Status = editingTest.Status
            };

            Tests.Add(newTest);
            EditingTest = new Test();
        }

        public void ClearTest()
        {
            EditingTest = new Test();
        }

        public void DeleteTest()
        {
            if (SelectedTest == null)
            {
                return;
            }

            if (selectedTest.DbTestId != null)
            {
                _dao.DeleteTest((int)selectedTest.DbTestId);
            }
            
            Tests.Remove(SelectedTest);

            EditingTest = new Test();
        }

        public void SaveTests()
        {
            foreach (var test in Tests)
            {
                if (test.DbTestId == null)
                {
                    _dao.CreateNewTest(test);
                }
                else
                {
                    _dao.UpdateTest((int)test.DbTestId, test);
                }
            }

            _dao.SaveChanges();
        }

        /// <summary>
        /// Set the status for the plan as completed if all test dates are filled in
        /// </summary>
        /// <returns></returns>
        public bool ApprovePlan()
        {
            // Check if all startdates are filled in
            // if (tests.Exists(t => t.StartDate == null))
            // Does not work for Observable Collections
            foreach (var test in Tests)
            {
                if (test.StartDate == null)
                {
                    MessageBox.Show("Please fill in all dates");
                    return false;
                }
            }

            foreach (var test in Tests)
            {
                test.Status = "Planned";

                if (test.EndDate == null)
                {
                    test.EndDate = test.StartDate;
                }
            }

            SelectedPlan.TestDivStatus = "Finished";
            SelectedPlan.TestDivPlanDate = DateTime.Now;

            // includes savechanges
            SaveTests();

            _dao.SetRqStatusIfComplete(SelectedPlan.IdRequest);

            return true;
        }

        private void SetVisibility()
        {
            bool isDoubleBooked = _dao.IsResourceDoubleBooked(editingTest);

            if (isDoubleBooked)
            {
                // Show
                DoubleBooked = Visibility.Visible;
                return;
            }

            // Hide
            DoubleBooked = Visibility.Hidden;

        }
          
    }
}
