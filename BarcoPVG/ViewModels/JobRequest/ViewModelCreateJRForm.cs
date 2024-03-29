﻿using Microsoft.Toolkit.Mvvm.Input;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using BarcoPVG.Models.Db;
using BarcoPVG.Models.Classes;
using BarcoPVG.Dao;

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
            if (_daoJR.GetJR(idRequest) == null)
            {

            }
            else
            {
                this._jr = _daoJR.GetJR(idRequest);

                List<RqRequestDetail> eutList = _daoJR.RqDetail(idRequest);
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
            var jobNatures = _daoJR.GetAllJobNatures();
            var divisions = _daoPerson.GetAllDivisions();
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

        // This function adds an new EUT instance into the GUI RequestForm
        // EUT in Database
        public void AddEUT()
        {
            EUTs.Add(new EUT());
        }

        // This function ensures that the existing data of an eut is read from the database and loaded into the requestForm xaml
        /// <param name="id"></param>
        /// <param name="jr"></param>
        public void FillEUT(RqRequest rq)
        {
            foreach (var objecten in _daoEUT.GetEut(rq))
            {
                EUTs.Add(objecten);
            }
        }

        // Clear all data in JR
        private void RefreshJR()
        {
            this.JR = _daoJR.GetNewJR();
       
            EUTs.Clear();
        }
        
        // deletes selected EUT via _selectedEut variable
        public void RemoveSelectedEUT(object obj)
        {
            EUTs.Remove(_selectedEUT);
        }
    }
}