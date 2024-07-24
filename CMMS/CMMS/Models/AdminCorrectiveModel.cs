using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMMS.Models
{
    public class AdminCorrectiveModel
    {

        [DisplayName("No Work Order")]
        public string id_woc { get; set; }

        [Required]
        public string id_machine { get; set; }
        public int id_callendar { get; set; }
        public string requested_by { get; set; }
        public string maintenance_by { get; set; }
        public string request_date { get; set; }
        public string deadline { get; set; }
        public string finish_date { get; set; }
        public int maintenance_cost { get; set; }

        [AllowHtml]
        [Required]
        public string desc_request { get; set; }

        [AllowHtml]
        public string desc_maintenance { get; set; }
        public string status { get; set; }
        public HttpPostedFileBase[] photos { get; set; }
    }
}