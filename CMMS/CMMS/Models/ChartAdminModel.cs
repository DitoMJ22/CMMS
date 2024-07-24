using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class ChartAdminModel
    {
        public string name { get; set; }
        public int dataWoc { get; set; }
        public int dataWop { get; set; }
        public int dataWocCost { get; set; }
        public int dataWopCost { get; set; }
    }
}