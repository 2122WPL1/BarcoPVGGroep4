using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcoDB_Admin.Models.Classes
{
    public class BarcoDivision
    {
        private bool _IsNull;
        public BarcoDivision()
        {
            EP_Coll = false;
            EP_LD = false;
            EP_NET_KAR = false;
            EP_NET_KND = false;
            EP_NET_SIL = false;
            EP_PROJ_CAV = false;
            ET_PROJ_DC = false;
            ET_PROJ_FRE = false;
            ET_PROJ_PD = false;
            ET_PROJ_SIM = false;
            ET_PROJ_V_M = false;
            ET_R_A_LED = false;
            HC = false;
            Silex = false;

            _IsNull = true;
        }

        public bool EP_Coll { get; set; }
        public bool EP_LD { get; set; }
        public bool EP_NET_KAR { get; set; }
        public bool EP_NET_KND { get; set; }
        public bool EP_NET_SIL { get; set; }
        public bool EP_PROJ_CAV { get; set; }
        public bool ET_PROJ_DC { get; set; }
        public bool ET_PROJ_FRE { get; set; }
        public bool ET_PROJ_PD { get; set; }
        public bool ET_PROJ_SIM { get; set; }
        public bool ET_PROJ_V_M { get; set; }
        public bool ET_R_A_LED { get; set; }
        public bool HC { get; set; }
        public bool Silex { get; set; }

     

        public ObservableCollection<bool> AllDivisions()
        {
            ObservableCollection<bool> _divTrue = new ObservableCollection<bool>();
            
            _divTrue.Add(EP_Coll);
            _divTrue.Add(EP_LD);
            _divTrue.Add(EP_NET_KAR);
            _divTrue.Add(EP_NET_KND);
            _divTrue.Add(EP_NET_SIL);
            _divTrue.Add(EP_PROJ_CAV);
            _divTrue.Add(ET_PROJ_DC);
            _divTrue.Add(ET_PROJ_FRE);
            _divTrue.Add(ET_PROJ_PD);
            _divTrue.Add(ET_PROJ_SIM);
            _divTrue.Add(ET_PROJ_V_M);
            _divTrue.Add(ET_R_A_LED);
            _divTrue.Add(HC);
            _divTrue.Add(Silex);

            return _divTrue;
        }

        public ObservableCollection<bool> AllTrueDivs()
        {
            ObservableCollection<bool> _divTrue = new ObservableCollection<bool>();

            addTrueToList(EP_Coll, _divTrue);
            addTrueToList(EP_LD, _divTrue);
            addTrueToList(EP_NET_KAR, _divTrue);
            addTrueToList(EP_NET_KND, _divTrue);
            addTrueToList(EP_NET_SIL, _divTrue);
            addTrueToList(EP_PROJ_CAV, _divTrue);
            addTrueToList(ET_PROJ_DC, _divTrue);
            addTrueToList(ET_PROJ_FRE, _divTrue);
            addTrueToList(ET_PROJ_PD, _divTrue);
            addTrueToList(ET_PROJ_SIM, _divTrue);
            addTrueToList(ET_PROJ_V_M, _divTrue);
            addTrueToList(ET_R_A_LED, _divTrue);
            addTrueToList(HC, _divTrue);
            addTrueToList(Silex, _divTrue);


            return _divTrue;
        }

        private void addTrueToList(bool okie, ObservableCollection<bool> list)
        {
            if (okie == true)
            {
                list.Add(okie);
            }
        }

        public bool IsNull
        {
            get
            {
                ObservableCollection<bool> list = new ObservableCollection<bool>();
                list.Add(EP_Coll);
                list.Add(EP_LD);
                list.Add(EP_NET_KAR);
                list.Add(EP_NET_KND);
                list.Add(EP_NET_SIL);
                list.Add(EP_PROJ_CAV);
                list.Add(ET_PROJ_DC);
                list.Add(ET_PROJ_FRE);
                list.Add(ET_PROJ_PD);
                list.Add(ET_PROJ_SIM);
                list.Add(ET_PROJ_V_M);
                list.Add(ET_R_A_LED);
                list.Add(HC);
                list.Add(Silex);

                _IsNull = list.FirstOrDefault(x => x == true) ? false : true;

                return _IsNull;
            }

            
        }

    }
}
