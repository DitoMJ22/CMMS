using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CMMS.Models
{
    public class MachineModel
    {
        [DisplayName("Asset Number")]
        public string no_asset { get; set; }

        [DisplayName("Name")]
        [Required]
        public string name { get; set; }

        [DisplayName("Model")]
        [Required]
        public string model { get; set; }

        [DisplayName("Merk")]
        [Required]
        public string merk { get; set; }

        [DisplayName("Year")]
        [Required]
        public string year { get; set; }

        [DisplayName("Condition")]
        [Required]
        public string condition { get; set; }

        [DisplayName("Lab")]
        [Required]
        public string lab { get; set; }

        public string status { get; set; }

        [DisplayName("Browse Photo")]
        public HttpPostedFileBase[] photos { get; set; }
    }
}