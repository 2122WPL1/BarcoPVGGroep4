using System.Collections.ObjectModel;
using BarcoPVG.Dao;

namespace BarcoPVG.ViewModels.JobRequest
{
    public class ViewModelCreateInternJRForm : AbstractViewModelContainer
    {
        private BarcoContext _context = new BarcoContext();

        public ObservableCollection<string> Divisions { get; set; }

        public ViewModelCreateInternJRForm(): base()
        {
            Init();
            Load();

            // JR = new JR
            RefreshJR();

        }

        private void Init()
        {
            Divisions = new ObservableCollection<string>();

        }

        private void Load()
        {
            var allDivisions = _daoPerson.GetAllDivisions();

            Divisions.Clear();

            foreach (var division in allDivisions)
            {
                Divisions.Add(division.Afkorting);
            }
        }
        private void RefreshJR()
        {
            this.JR = _daoJR.GetNewJR();
            
        }
    }
}
