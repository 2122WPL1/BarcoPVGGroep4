using Microsoft.Toolkit.Mvvm.Input;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BarcoPVG.Models.Classes;
using BarcoPVG.Viewmodels.JobRequest;
using BarcoPVG.Viewmodels.TestGUI;
using BarcoPVG.Viewmodels.Planning;
using BarcoPVG.ViewModels.DatabaseManagement;

namespace BarcoPVG.Viewmodels
{
    // Kaat
    class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;
        public BarcoUser User { get; set; }


        // TODO: check if ICommand also works
        public DelegateCommand DisplayNewJRCommand { get; set; }
        public DelegateCommand DisplayExistingJRCommand { get; set; }
        public DelegateCommand DisplayEmployeeStartupCommand { get; set; }
        public DelegateCommand DisplayPlannerStartupCommand { get; set; }
        public DelegateCommand DisplayTesterPlanCommand { get; set; }
        public DelegateCommand DisplayTesterTestCommand { get; set; }
        public DelegateCommand DisplayDevStartupCommand { get; set; }
        public DelegateCommand SaveJrCommand { get; set; }
        public DelegateCommand ApproveJRCommand { get; set; }
        public DelegateCommand DisplayTestPlanningCommand { get; set; }
        public DelegateCommand SaveTestsAndReturnCommand { get; set; }
        public DelegateCommand ApprovePlanAndReturnCommand { get; set; }
        public DelegateCommand TesterReturnCommand { get; set; }
        // Amy & Jarne
        public DelegateCommand AddUserCommand { get; set; }
        public DelegateCommand RemoveUserCommand { get; set; }
        public DelegateCommand AddResourceCommand { get; set; }
        public DelegateCommand RemoveResourceCommand { get; set; }
        public DelegateCommand DatabaseManagementCommand { get; set; }
        public DelegateCommand SaveAddUserCommand { get; set; }
        public DelegateCommand DisplayDatabaseManagementStartupCommand { get; set; }

        // Visibility of buttons
        public Visibility NewRequests { get; set; }
        public Visibility ApproveRequests { get; set; }
        public Visibility Test { get; set; }
        public Visibility SeeAll { get; set; }
        //Jarne
        public Visibility Data { get; set; }

        public ViewModelMain()
        {
            this.User = _dao.BarcoUser;

            DisplayNewJRCommand = new DelegateCommand(DisplayNewJR);
            DisplayExistingJRCommand = new DelegateCommand(DisplayExistingJR);
            DisplayEmployeeStartupCommand = new DelegateCommand(DisplayEmployeeStartup);
            DisplayPlannerStartupCommand = new DelegateCommand(DisplayPlannerStartup);
            DisplayTesterPlanCommand = new DelegateCommand(DisplayTesterPlan);
            DisplayTesterTestCommand = new DelegateCommand(DisplayTesterTest);
            DisplayDevStartupCommand = new DelegateCommand(DisplayDevStartup);
            ApproveJRCommand = new DelegateCommand(ApproveJR);
            DisplayTestPlanningCommand = new DelegateCommand(DisplayTestPlanning);
            SaveTestsAndReturnCommand = new DelegateCommand(SaveTestsAndReturn);
            ApprovePlanAndReturnCommand = new DelegateCommand(ApprovePlanAndReturn);
            TesterReturnCommand = new DelegateCommand(TesterReturn);
            //Jarne & Amy
            AddUserCommand = new DelegateCommand(DisplayAddUser);
            RemoveUserCommand = new DelegateCommand(DisplayRemoveUser);
            AddResourceCommand = new DelegateCommand(DisplayAddResource);
            RemoveResourceCommand = new DelegateCommand(DisplayRemoveResource);
            DatabaseManagementCommand = new DelegateCommand(DisplayDatabaseManagement);
            SaveAddUserCommand = new DelegateCommand(DisplayAddUser);
            DisplayDatabaseManagementStartupCommand = new DelegateCommand(DisplayDatabaseManagementStartup);

            SetWindowProperties();
        }

        // Getters/Setters
        public AbstractViewModelBase ViewModel 
        { 
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnpropertyChanged();
            }
        }

        // Command methods
        // TODO: add method to switch return window based on function
        public void DisplayNewJR()
        {
            SaveJrCommand = new DelegateCommand(InsertJr);
            this.ViewModel = new ViewModelCreateJRForm();
        }

        public void DisplayNewInternalJR()
        {
            SaveJrCommand = new DelegateCommand(InsertInternalJr);
            this.ViewModel = new ViewModelCreateJRForm(true);
        }

        public void DisplayExistingJR() //Sander: Foutmelding wanneer er niets geselecteerd wordt
        {

            SaveJrCommand = new DelegateCommand(UpdateJr);


            var ExistingJrId = ((AbstractViewModelCollectionRQ)this.ViewModel).SelectedJR.IdRequest;

            if (((AbstractViewModelCollectionRQ)this.ViewModel).SelectedJR.ExpectedEnddate != new DateTime()) //als de verwachte einddatum niet geset is dan geeft hij een foutmelding
            {
                if (this.ViewModel is ViewModelApproveJRQueue)
                {
                    this.ViewModel = new ViewModelApproveJRForm(ExistingJrId);
                }
                else
                {

                    this.ViewModel = new ViewModelCreateJRForm(ExistingJrId);
                }
            }
            else
            {
                MessageBox.Show("Geen JR geselecteerd");

            }
        }

        public void DisplayEmployeeStartup()
        {
            this.ViewModel = new ViewModelCreateJRQueue();
        }

        public void DisplayPlannerStartup()
        {
            this.ViewModel = new ViewModelApproveJRQueue();
        }
        public void DisplayTesterPlan()
        {
            this.ViewModel = new ViewModelPlanTestQueue();
        }

        public void DisplayTesterTest()
        {
            this.ViewModel = new ViewModelUpdateTestQueue();
        }

        public void DisplayDevStartup()
        {
            this.ViewModel = new ViewModelDevelopment();
        }

        //Jarne & Amy
        public void DisplayAddUser()
        {
            SaveAddUserCommand = new DelegateCommand(InsertUser);
            this.ViewModel = new ViewModelDatabaseAddUser();
        }
        public void DisplayRemoveUser()
        {
            this.ViewModel = new ViewModelDatabaseRemoveUser();
        }

        public void DisplayAddResource()
        {
            this.ViewModel = new ViewModelDatabaseAddResource();
        }

        public void DisplayRemoveResource()
        {
            this.ViewModel = new ViewModelDatabaseRemoveResource();
        }
        public void DisplayDatabaseManagement()
        {
            this.ViewModel = new ViewModelDatabaseManagement();
        }
        public void DisplayDatabaseManagementStartup()
        {
            this.ViewModel = new ViewModelDatabaseManagement();
        }

        // JR CRUD
        // Command functions
        // Adds and stores a job request and switches windows
        public void InsertJr()
        {
            var jr = _dao.AddJobRequest(((AbstractViewModelContainer)this.ViewModel).JR); // SaveChanges included in function
            int count = 0;

            foreach (var thisEUT in ((AbstractViewModelContainer)this.ViewModel).EUTs)
            {
                count++;
                _dao.AddEutToRqRequest(jr, thisEUT, count.ToString());
            }

            // Here we call the SaveChanges method, so that we can link several EUTs to one JR
            _dao.SaveChanges();
            DisplayDevStartup();
        }

        // Change so no JR and no 
        public void InsertInternalJr()
        {
            var jr = _dao.AddJobRequest(((AbstractViewModelContainer)this.ViewModel).JR); // SaveChanges included in function

            jr.JrStatus = "In Plan";
            
            int count = 0;
            foreach (var thisEUT in ((AbstractViewModelContainer)this.ViewModel).EUTs)
            {
                count++;
                _dao.AddEutToRqRequest(jr, thisEUT, count.ToString());
            }

            // Here we call the SaveChanges method, so that we can link several EUTs to one JR
            _dao.SaveChanges();

            _dao.ApproveInternalRequest(jr.IdRequest);

            DisplayDevStartup();
        }

        // Updates existing job request and switches windows
        public void UpdateJr()
        {
            string error = _dao.UpdateJobRequest(((AbstractViewModelContainer)this.ViewModel).JR); // SaveChanges included in function

            if (error == null)
            {
                DisplayDevStartup();
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        // Switch screen for planner
        // Kaat
        public void ApproveJR()
        {
            int jrId = ((AbstractViewModelContainer)this.ViewModel).JR.IdRequest;

            _dao.ApproveRequest(jrId);

            this.ViewModel = new ViewModelApproveJRQueue();
        }

        // Switch to test planning for tester
        public void DisplayTestPlanning()
        {
            // get id from JR
            var plan = ((ViewModelPlanTestQueue)this.ViewModel).SelectedPlan;

            this.ViewModel = new ViewModelPlanTestForm(plan);
        }

        public void SaveTestsAndReturn()
        {
            ((ViewModelPlanTestForm)this.ViewModel).SaveTests();
            this.ViewModel = new ViewModelPlanTestQueue();
        }

        public void ApprovePlanAndReturn()
        {
            var isSaved = ((ViewModelPlanTestForm)this.ViewModel).ApprovePlan();

            if (isSaved)
            {
                this.ViewModel = new ViewModelPlanTestQueue();
            }
        }

        public void TesterReturn()
        {
            _dao.RemoveChanges();
            this.ViewModel = new ViewModelPlanTestQueue();
        }

        private void SetWindowProperties()
        {
            switch (_dao.BarcoUser.Function)
            {

                //Jarne
                case "DATA":
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Visible;
                    Data = Visibility.Visible;

                    this.ViewModel = new ViewModelDatabaseManagement();

                    break;
                case "DEV":
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Visible;
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelDevelopment();

                    break;
                case "TEST":
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Hidden;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Hidden;
                    Data = Visibility.Hidden;

                    DisplayNewJRCommand = new DelegateCommand(DisplayNewInternalJR);

                    this.ViewModel = new ViewModelPlanTestQueue();

                    break;
                case "PLAN":
                    NewRequests = Visibility.Hidden;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Hidden;
                    SeeAll = Visibility.Hidden;
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelApproveJRQueue();

                    break;

                default:
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Hidden;
                    Test = Visibility.Hidden;
                    SeeAll = Visibility.Hidden;

                    this.ViewModel = new ViewModelCreateJRQueue();

                    break;
            }
        }



        public void InsertUser()
        {

        }

    }
}
