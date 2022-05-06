using BarcoPVG.Models.Classes;
using BarcoPVG.Models.Db;
using BarcoPVG.Viewmodels.JobRequest;
using BarcoPVG.Viewmodels.Planning;
using BarcoPVG.Viewmodels.TestGUI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows;
using BarcoPVG.Dao;


namespace BarcoPVG.Viewmodels
{
    // Kaat
    class ViewModelMain : AbstractViewModelBase
    {
        //protected DAO _dao = DAO.Instance();

        private AbstractViewModelBase _viewModel;

        public BarcoUser User { get; set; }

        // TODO: check if ICommand also works
        public DelegateCommand Exit { get; set; }
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
        public DelegateCommand DisplayDatabaseManagementStartupCommand { get; set; } //button vanboven

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
        // TODO: add method to switch return window based on function
        public void DisplayNewJR()
        {
            SaveJrCommand = new DelegateCommand(InsertJr);
            this.ViewModel = new ViewModelCreateJRForm();
        }

        public void DisplayExistingJR() //Sander: Foutmelding wanneer er niets geselecteerd wordt
        {

            SaveJrCommand = new DelegateCommand(UpdateJr);


            var ExistingJrId = ((AbstractViewModelCollectionRQ) this.ViewModel).SelectedJR.IdRequest;

            if (((AbstractViewModelCollectionRQ) this.ViewModel).SelectedJR.ExpectedEnddate !=
                new DateTime()) //als de verwachte einddatum niet geset is dan geeft hij een foutmelding
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

        // JR CRUD
        // Command functions
        // Adds and stores a job request and switches windows
        private bool CheckCreateRequirements(RqRequest jr, out List<EUT> eUTs1)
        {
            List<EUT> eUts2 = new List<EUT>();

            bool passed = false;
            if (jr.Requester.Length > 10)
            {
                MessageBox.Show("Requester is longer than the allowed length(10)");
            }
            else
            {
                if (!(
                        jr.BarcoDivision == String.Empty || jr.JobNature == String.Empty ||
                        jr.EutPartnumbers == String.Empty || jr.NetWeight == String.Empty ||
                        jr.GrossWeight == String.Empty || jr.EutProjectname == String.Empty ||
                        jr.HydraProjectNr == String.Empty
                    ))
                {
                    if (jr.ExpectedEnddate.Date !=
                        DateTime.Now
                            .Date) //geeft nooit null | de tijd van vandaag door datetime now te gebruiken | datetime now en enddate 1 milliseconde verschil | nu tijdelijke if clause
                    {
                        if (((AbstractViewModelContainer) this.ViewModel).EUTs.Count == 0) //all EUT from JR
                        {
                            MessageBox.Show("Er moet tenminste 1 eut zijn");
                        }
                        else
                        {
                            foreach (EUT eUT in ((AbstractViewModelContainer) this.ViewModel).EUTs)
                            {
                                if (!CheckEUTRequirements(eUT))
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
                        MessageBox.Show("geef een datum in voor de jr");
                    }
                }
                else
                {
                    MessageBox.Show("Alle verplichte gegevens moeten ingevuld worden");
                }
            }

            eUTs1 = eUts2;
            return passed;
        }

        private bool CheckEUTRequirements(EUT eut)
        {
            bool passed = false;
            foreach (var thisEUT in ((AbstractViewModelContainer) this.ViewModel).EUTs)
            {
                if (thisEUT.AvailabilityDate != null)
                {
                    if (thisEUT.ECO || thisEUT.SAV || thisEUT.EMC || thisEUT.ENV || thisEUT.PCK || thisEUT.REL ||
                        thisEUT.SAV)
                    {
                        passed = true;
                    }
                    else
                    {
                        MessageBox.Show("selecteer een devision bij alle de euts");
                    }
                }
                else
                {
                    MessageBox.Show("geen datum voor 1 van de eut's geslescteerd");
                }
            }

            return passed;
        }

        //Sander, Jarne
        //aanmaken van een JRNummer zodat deze ingevuld kan worden 
        private string CreateJRNummer(RqRequest jr)
        {
            //
            string JrNumber = "JR" + _dao.BarcoUser.Function;

            for (int i = jr.IdRequest.ToString().Length; i <= 5; i++)
            {
                JrNumber += "0";
            }

            JrNumber += _dao.GetJR(jr).IdRequest;

            return JrNumber;
        }

        public void InsertJr() // aanmaken job request
        {
            var jr = _daoJR.AddJobRequest(
                ((AbstractViewModelContainer) this.ViewModel) //ID request wordt automatisch 0 voor een of andere reden
                .JR); // SaveChanges included in function
            int count = 0;

            {
                //jr.JrNumber = CreateJRNummer(jr); //jr ID wordt automatisch toegevoegd bij savecnages waardoor deze niet ka nwerken

                List<EUT> euts = new List<EUT>();
                if (CheckCreateRequirements(jr, out euts))
                {
                    foreach (EUT eut in euts)
                    {
                        _dao.AddEutToRqRequest(jr, eut, count.ToString());
                        count++;
                    }

                    _dao.SaveChanges();
                    DisplayDevStartup();
                }
                // Here we call the SaveChanges method, so that we can link several EUTs to one JR
            }
        }

        public void InsertInternalJr()
        {
            var jr = _dao.AddJobRequest(((AbstractViewModelContainer) this.ViewModel)
                .JR); // SaveChanges included in function

            jr.JrStatus = "In Plan";

            int count = 0;
            foreach (var thisEUT in ((AbstractViewModelContainer) this.ViewModel).EUTs)
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
            string error =
                _dao.UpdateJobRequest(((AbstractViewModelContainer) this.ViewModel)
                    .JR); // SaveChanges included in function

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
            int jrId = ((AbstractViewModelContainer) this.ViewModel).JR.IdRequest;

            _dao.ApproveRequest(jrId);

            this.ViewModel = new ViewModelApproveJRQueue();
        }

        // Switch to test planning for tester
        public void DisplayTestPlanning() //sander: foutafhandeling wanneer er niets geselecteerd is
        {
            // get id from JR

            var plan = ((ViewModelPlanTestQueue) this.ViewModel).SelectedPlan;
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
            ((ViewModelPlanTestForm) this.ViewModel).SaveTests();
            this.ViewModel = new ViewModelPlanTestQueue();
        }

        public void ApprovePlanAndReturn()
        {
            var isSaved = ((ViewModelPlanTestForm) this.ViewModel).ApprovePlan();

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
                ////Jarne
                ////aanmaken van een nieuwe view die DATA voor de Visibility van de database button
                //case "DATA":
                //    NewRequests = Visibility.Visible;
                //    ApproveRequests = Visibility.Visible;
                //    Test = Visibility.Visible;
                //    SeeAll = Visibility.Visible;
                //    Data = Visibility.Visible;
                //
                //    break;
                case "DEV": // Developer -> Developer voor dit programma 
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Visible;
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelDevelopment();

                    break;
                case "TEST": // Test team -> om de geplande JR te plannen voor de test 
                    NewRequests = Visibility.Visible;
                    ApproveRequests = Visibility.Hidden;
                    Test = Visibility.Visible;
                    SeeAll = Visibility.Hidden;
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelPlanTestQueue();

                    break;
                case "PLAN": // Planning -> om plans goed te keuren
                    NewRequests = Visibility.Hidden;
                    ApproveRequests = Visibility.Visible;
                    Test = Visibility.Hidden;
                    SeeAll = Visibility.Hidden;
                    Data = Visibility.Hidden;

                    this.ViewModel = new ViewModelApproveJRQueue();

                    break;
                default: // Normale gebuikers -> die kan enkele JR aan te vragen
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
