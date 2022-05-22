using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;
using BarcoPVG.ViewModels.JobRequest;
using BarcoPVG.ViewModels.Planning;
using BarcoPVG.ViewModels.TestGUI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows;

namespace BarcoPVG.ViewModels
{
    // Kaat
    class ViewModelMain : AbstractViewModelBase
    {
        private AbstractViewModelBase _viewModel;

        public BarcoUser User { get; set; }

        public DelegateCommand Exit { get; set; }
        public DelegateCommand DisplayNewJRCommand { get; set; }
        public DelegateCommand DisplayNewInternJRCommand { get; set; }  // Eakarch
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

        // Visibility of buttons
        public Visibility NewRequests { get; set; }
        public Visibility ApproveRequests { get; set; }
        public Visibility Test { get; set; }

        public Visibility SeeAll { get; set; }

        public ViewModelMain()
        {
            this.User = _daoLogin.BarcoUser;

            DisplayNewJRCommand = new DelegateCommand(DisplayNewJR);
            DisplayNewInternJRCommand = new DelegateCommand(DisplayNewInternJR);
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
            Exit = new DelegateCommand(exit);

            SetWindowProperties();
        }

        public void exit()
        {
            Environment.Exit(0);
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
        public void DisplayNewJR()
        {
            SaveJrCommand = new DelegateCommand(InsertJr);
            this.ViewModel = new ViewModelCreateJRForm();
        }

        public void DisplayExistingJR() //Sander: error message when nothing is selected
        {

            SaveJrCommand = new DelegateCommand(UpdateJr);


            var ExistingJrId = ((AbstractViewModelCollectionRQ)this.ViewModel).SelectedJR.IdRequest;

            if (((AbstractViewModelCollectionRQ)this.ViewModel).SelectedJR.ExpectedEnddate !=
                new DateTime()) //if the expected end-date is null then error message
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
                MessageBox.Show("No JR selected");
            }
        }

        public void DisplayEmployeeStartup()
        {
            this.ViewModel = new ViewModelCreateJRQueue();
        }

        //Eakarach
        private void DisplayNewInternJR()
        {
            SaveJrCommand = new DelegateCommand(InsertInternalJr);
            this.ViewModel = new ViewModelCreateInternJRForm();
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

        // JR CRUD
        // Command functions
        // Adds and stores a job request and switches windows
        private bool CheckCreateRequirements(RqRequest jr, out List<EUT> eUTs1)
        {
            List<EUT> eUts2 = new List<EUT>();

            bool passed = false;
            if (jr.Requester.Length > 20)
            {
                MessageBox.Show("Requester is longer than the allowed length (20)");
            }
            else
            {
                if (!(
                        //Jarne
                        //checking if fields JR are null
                        jr.BarcoDivision == String.Empty || jr.JobNature == String.Empty ||
                        jr.EutProjectname == String.Empty || jr.HydraProjectNr == String.Empty
                    ))
                {
                    if (jr.ExpectedEnddate.Date != DateTime.Now.Date) //never returns null | today's time by using datetime now | datetime now and end-date 1 milliseconde different
                    {
                        if (((AbstractViewModelContainer)this.ViewModel).EUTs.Count == 0) //all EUT from JR
                        {
                            MessageBox.Show("there must be at least 1 EUT");
                        }
                        else
                        {
                            foreach (EUT eUT in ((AbstractViewModelContainer)this.ViewModel).EUTs)
                            {
                                if (!CheckEUTRequirements(eUT, jr))
                                {
                                    passed = false;
                                    break;
                                }
                                else
                                {
                                    passed = true;
                                }
                                eUts2.Add(eUT);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter a date");
                    }
                }
                else
                {
                    MessageBox.Show("All non-optional fields must be filled in");
                }
            }
            eUTs1 = eUts2;
            return passed;
        }

        private bool CheckEUTRequirements(EUT eut, RqRequest jr)
        {
            bool passed = false;
            foreach (var thisEUT in ((AbstractViewModelContainer)this.ViewModel).EUTs)
            {
                if (thisEUT.AvailabilityDate != null && thisEUT.AvailabilityDate < jr.ExpectedEnddate)
                {
                    if (thisEUT.ECO || thisEUT.SAV || thisEUT.EMC || thisEUT.ENV || thisEUT.PCK || thisEUT.REL ||
                        thisEUT.SAV)
                    {
                        passed = true;
                    }
                    else
                    {
                        MessageBox.Show("Select a division with every EUT");
                    }
                }
                else
                {
                    MessageBox.Show("Check the EUT's selected date");
                }
            }
            return passed;
        }

        public void InsertJr() // creating a job request
        {
            var jr = _daoJR.AddJobRequest(
                ((AbstractViewModelContainer)this.ViewModel) //TODO ID request is automatically 0 for some reason
                .JR); // SaveChanges included in function
            int count = 0;

            List<EUT> euts = new List<EUT>();
            if (CheckCreateRequirements(jr, out euts))
            {
                foreach (EUT eut in euts)
                {
                    _daoEUT.AddEutToRqRequest(jr, eut, count.ToString());
                    count++;
                }
                _daoJR._context.RqRequests.Add(jr);
                // Here we call the SaveChanges method, so that we can link several EUTs to one JR
                _daoJR.SaveChanges();
                DisplayEmployeeStartup();
                //Jarne switch for openening window based on who's logged in
                switch (_daoLogin.BarcoUser.Division)
                {
                    case "DEV":
                        DisplayDevStartup();
                        break;
                    case "TEST":
                        DisplayEmployeeStartup();
                        break;
                    case "PLAN":
                        DisplayPlannerStartup();
                        break;
                    default: // extern
                        DisplayEmployeeStartup();
                        break;
                }
            }
        }

        public void InsertInternalJr()
        {
            if (((AbstractViewModelContainer)this.ViewModel).JR.BarcoDivision == null ||
                ((AbstractViewModelContainer)this.ViewModel).JR.BarcoDivision == "" ||
                ((AbstractViewModelContainer)this.ViewModel).JR.EutProjectname == null ||
                ((AbstractViewModelContainer)this.ViewModel).JR.EutProjectname == "")
            {
                MessageBox.Show("Please fill everything");
            }
            else
            {
                _daoInternalJr.AddInternJobRequest(((AbstractViewModelContainer)this.ViewModel).JR); // SaveChanges included in function
                DisplayTesterPlan();
            }
        }

        // Updates existing job request and switches windows
        public void UpdateJr()
        {
            var jr = _daoJR.AddJobRequest(
               ((AbstractViewModelContainer)this.ViewModel)
               .JR); // SaveChanges included in function
            int count = 0;
            {
                //jr.JrNumber = CreateJRNummer(jr); 

                List<EUT> euts = new List<EUT>();
                if (CheckCreateRequirements(jr, out euts))
                {
                    foreach (EUT eut in euts)
                    {
                        _daoEUT.AddEutToRqRequest(jr, eut, count.ToString());
                        count++;
                    }
                    string error = _daoJR.UpdateJobRequest(((AbstractViewModelContainer)this.ViewModel).JR); // SaveChanges included in function

                    if (error == null)
                    {
                        DisplayEmployeeStartup();
                    }
                    else
                    {
                        MessageBox.Show(error);
                    }
                }
                // Here we call the SaveChanges method, so that we can link several EUTs to one JR
            }
        }

        // Switch screen for planner
        // Kaat
        public void ApproveJR()
        {
            int jrId = ((AbstractViewModelContainer)this.ViewModel).JR.IdRequest;

            _daoApproval.ApproveRequest(jrId);

            this.ViewModel = new ViewModelApproveJRQueue();
        }

        // Switch to test planning for tester
        public void DisplayTestPlanning() //sander: error catch when nothing is selected
        {
            // get id from JR

            var plan = ((ViewModelPlanTestQueue)this.ViewModel).SelectedPlan;
            if (plan.JrNr != null)
            {
                this.ViewModel = new ViewModelPlanTestForm(plan);
            }
            else
            {
                MessageBox.Show("No JR selected");
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
            string i = _daoLogin.BarcoUser.Functie;
            switch (i)
            {
                case "DEV": //Developer
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Visible;

                    this.ViewModel = new ViewModelDevelopment();

                    break;
                case "TEST": //Test team
                    NewRequests = Visibility.Hidden;
                    ApproveRequests = Visibility.Hidden;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Hidden;

                    this.ViewModel = new ViewModelPlanTestQueue();

                    break;
                case "PLAN": //Planning
                    NewRequests = Visibility.Hidden;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Hidden;
                    SeeAll = Visibility.Hidden;

                    this.ViewModel = new ViewModelApproveJRQueue();

                    break;
                default: //Standard user
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Hidden;
                    Test = Visibility.Hidden;
                    SeeAll = Visibility.Hidden;

                    this.ViewModel = new ViewModelCreateJRQueue();
                    break;
            }
        }
    }
}