using Microsoft.Toolkit.Mvvm.Input;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using BarcoPVG.Models;
using BarcoPVG.Views;
using BarcoPVG.Models.Db;
using BarcoPVG.Models.Classes;
using BarcoPVG.Dao;
using BarcoPVG.ViewModels;

namespace BarcoPVG.ViewModels.JobRequest
{
    public class ViewModelCreateJRForm : AbstractViewModelContainer
    {
        private BarcoContext _context = new BarcoContext();
        // Combobox contents
        public ObservableCollection<string> JobNatures { get; set; }
        public ObservableCollection<string> Divisions { get; set; }

        // Command declaration
        // RelayCommand<Class> takes object of type class as input
        public RelayCommand<Window> AddJobRequestCommand { get; set; }
        public RelayCommand<Window> CancelCommand { get; set; }

        // ICommand does not take pinput
        public ICommand AddEUTCommand { get; set; }
        public ICommand RemoveEUTCommand { get; set; }
        public ICommand RefreshJRCommand { get; set; }


        // Constructor for new JR
        public ViewModelCreateJRForm(bool isInternalRequest = false) : base()
        {
            Init();
            Load();

            // JR = new JR
            RefreshJR();

            _jr.InternRequest = isInternalRequest;
        }

        // Constructor for existing JR
        public ViewModelCreateJRForm(int idRequest) : base()
        {
            Init();
            Load();

            // Look for JR with correct ID
            if (_dao.GetJR(idRequest) == null)
            {

            }
            else
            {
                this._jr = _dao.GetJR(idRequest);


                List<RqRequestDetail> eutList = _dao.RqDetail(idRequest);
                // We use a foreach to loop over every item in the eutList
                // And link the user inputed data to the correct variables
                var request = new RqRequest();
                foreach (var id in eutList)
                {
                    // Use DAO? --> base class
                    request = _context.RqRequests.FirstOrDefault(e => e.IdRequest == id.IdRequest);

                }
                FillEUT(request);
            }
        }

        // Code reused in both constructors
        private void Init()
        {
            // Collection initialization
            JobNatures = new ObservableCollection<string>();
            Divisions = new ObservableCollection<string>();

            // Command initialization
            RefreshJRCommand = new DelegateCommand(RefreshJR);
            AddEUTCommand = new DelegateCommand(AddEUT);
            RemoveEUTCommand = new Command((param) => RemoveSelectedEUT(param));
        }

        // Loads jobNatures, divisions in cbb
        public void Load()
        {
            var jobNatures = _dao.GetAllJobNatures();
            var divisions = _dao.GetAllDivisions();
            JobNatures.Clear();
            Divisions.Clear();

            foreach (var jobNature in jobNatures)
            {
                JobNatures.Add(jobNature.Nature);
            }

            foreach (var division in divisions)
            {
                Divisions.Add(division.Afkorting);
            }
        }

        /// <summary>
        /// This function adds an new EUT instance into the GUI RequestForm
        /// EUT in Database
        /// </summary>
        public void AddEUT()
        {
            EUTs.Add(new EUT());
        }

        /// <summary>
        /// This function ensures that the existing data of an eut is read from the database and loaded into the requestForm xaml
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jr"></param>
        public void FillEUT( RqRequest rq)
        {
            foreach (var objecten in _dao.GetEut(rq))
            {
                EUTs.Add(objecten);
            }
        }


        /// <summary>
        /// Clear all data in JR
        /// </summary>
        private void RefreshJR()
        {
      
            this.JR = _dao.GetNewJR();
       
            EUTs.Clear();
        }

        /// <summary>
        /// deletes selected EUT via _selectedEut variable
        /// </summary>
        public void RemoveSelectedEUT(object obj)
        {
            EUTs.Remove(_selectedEUT);
        }

   
    }
}
