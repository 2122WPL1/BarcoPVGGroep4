using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Prism.Commands;
using BarcoPVG.Models;
using BarcoPVG;
using BarcoPVG.Models.Db;
using BarcoPVG.Models.Classes;
using BarcoPVG.Dao;

namespace BarcoPVG.Viewmodels.JobRequest
{
    class ViewModelApproveJRForm : AbstractViewModelContainer
    {
        private readonly BarcoContext _context = new BarcoContext();

        // Combobox contents
        public ObservableCollection<string> JobNatures { get; set; }
        public ObservableCollection<string> Divisions { get; set; }

        // ICommand does not take pinput
        public ICommand AddEUTCommand { get; set; }
        public ICommand RemoveEUTCommand { get; set; }
        public ICommand RefreshJRCommand { get; set; }
        public ICommand AddMockEUTCommand { get; set; }

        // Constructor for existing JR
        // Planner only works with existing JRs
        public ViewModelApproveJRForm(int idRequest) : base()
        {
            // Fill in dropdown menu's
            JobNatures = new ObservableCollection<string>();
            Divisions = new ObservableCollection<string>();

            Load();
            AddEUTCommand = new DelegateCommand(AddEUT);
            RemoveEUTCommand = new DelegateCommand(RemoveSelectedEUT);

            // Look for JR with correct ID
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

            _dao.PrintPvg(idRequest,_jr);
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
        public void FillEUT(RqRequest rq)
        {
            foreach (var objecten in _dao.GetEut(rq))
            {
                EUTs.Add(objecten);
            }
        }

        /// <summary>
        /// deletes selected EUT via _selectedEut variable
        /// </summary>
        public void RemoveSelectedEUT()
        {
            EUTs.Remove(SelectedEUT);
        }
    }
}
