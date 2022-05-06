using BarcoPVG.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarcoPVG.ViewModels.JobRequest
{
    public class ViewModelCreateInternJRForm : AbstractViewModelContainer
    {
        

        public ObservableCollection<string> Divisions { get; set; }

        public ViewModelCreateInternJRForm(): base()
        {
            Init();
            Load();
        }

        private void Init()
        {
            Divisions = new ObservableCollection<string>();

        }

        private void Load()
        {
            var allDivisions = _dao.GetAllDivisions();

            Divisions.Clear();

            foreach (var division in allDivisions)
            {
                Divisions.Add(division.Afkorting);
            }
        }
    }
}
