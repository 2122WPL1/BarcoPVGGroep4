using System;
using System.Collections.Generic;
using System.Text;

namespace BarcoDB_Admin.Models.Classes
{
    public class Test
    {
        public int? DbTestId { get; set; }
        public string Description { get; set; }
        public int RQId { get; set; }
        public string TestDivision { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Resource { get; set; }
        public string Status { get; set; }

        public Test()
        {
            Status = "Unconfirmed";
        }
    }
}
