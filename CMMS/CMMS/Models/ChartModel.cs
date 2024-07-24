using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class ChartModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public DateTime waktu { get; set; }
        public float temperature { get; set; }

        public string id_machine { get; set; }
        public List<float> temperatures { get; set; }
    }
}