﻿using BarcoDB_Admin.Models.Db;
using System.Linq;

namespace BarcoDB_Admin.ViewModels.Edit
{
    //Amy
    public class ViewModelEditDevision : AbstractViewModelContainer
    {
        public RqBarcoDivision SelectedDivision { get; set; }
        public bool _IsEnable1; 
        public bool IsEnable1
        {
            get
            { 
                return _IsEnable1;
            }
            set
            {
                _IsEnable1 = value;
                OnpropertyChanged();
            }
        }

        public ViewModelEditDevision(string Afkorting) : base()
        {
            Division = _daoDivision.GetAllDivisions().FirstOrDefault(x => x.Afkorting == Afkorting);
            _IsEnable1 = false; // Set to false because we can't let primary to be changed.
        }

    }
}
