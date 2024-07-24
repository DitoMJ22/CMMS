using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class LabModel
    {
        [DisplayName("ID")]
        public string id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string name { get; set; }

        [DisplayName("UPT")]
        [Required]
        public int upt { get; set; }

        public string uptname { get; set; }

        [DisplayName("PIC")]
        [Required]
        public string pic { get; set; }

        public string status { get; set; }
    }
}