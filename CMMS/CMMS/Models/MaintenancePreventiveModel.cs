using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMMS.Models
{
    public class MaintenancePreventiveModel
    {
        [DisplayName("No Work Order")]
        public string id_wop { get; set; }

        [Required]
        public string id_machine { get; set; }
        public int id_callendar { get; set; }
        public string requested_by { get; set; }
        public string maintenance_by { get; set; }
        public string schedule_date { get; set; }
        public string start_date { get; set; }
        public string finish_date { get; set; }
        public int maintenance_cost { get; set; }
        public int sparepart_cost { get; set; }
        public int cost { get; set; }

        [AllowHtml]
        [Required]
        public string description { get; set; }

        [AllowHtml]
        public string desc_maintenance { get; set; }
        public string status { get; set; }
    }
}