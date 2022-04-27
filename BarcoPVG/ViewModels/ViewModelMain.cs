using BarcoPVG.Models.Classes;
using BarcoPVG.Viewmodels.JobRequest;
using BarcoPVG.Viewmodels.Planning;
using BarcoPVG.Viewmodels.TestGUI;
using BarcoPVG.ViewModels.DatabaseManagement;
using Prism.Commands;
using System;
using System.Windows;



namespace BarcoPVG.Viewmodels
{
    // Kaat
    class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;
        private AbstractViewModelBase _DataBase; //amy
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
        public DelegateCommand DisplayDatabaseManagementStartupCommand { get; set; }
        public DelegateCommand DisplayDatabaseUserCommand { get; set; }
        public DelegateCommand DisplayDataResourceCommand { get; set; }



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
            DisplayDatabaseManagementStartupCommand = new DelegateCommand(DisplayDatabaseManagementStartup);
            DisplayDatabaseUserCommand = new DelegateCommand(DisplayDatabaseUserStartup);
            DisplayDataResourceCommand = new DelegateCommand(DisplayDataBaseResourceStartup);

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
        //Amy
        public AbstractViewModelBase DataBase
        {
            get => _DataBase;
            set 
            {
                _DataBase = value;
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
        //Amy
        public void DisplayDatabaseManagementStartup()
        {
            this.ViewModel = new ViewModelDatabaseManagement();
        }

        public void DisplayDatabaseUserStartup()
        {
            this.DataBase = new ViewModelDBUser();
        }
        public void DisplayDataBaseResourceStartup()
        {
            this.DataBase = new ViewModelDBResource();
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
        public void DisplayTestPlanning() //sander: foutafhandeling wanneer er niets geselecteerd is
        {
            // get id from JR

            var plan = ((ViewModelPlanTestQueue)this.ViewModel).SelectedPlan;
            if (plan.JrNr != null)
            {
                this.ViewModel = new ViewModelPlanTestForm(plan);
            }
            else
            {
                MessageBox.Show("Geen planning geselecteerd");
            }
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
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelCreateJRQueue();

                    break;
            }
        }




    }
}