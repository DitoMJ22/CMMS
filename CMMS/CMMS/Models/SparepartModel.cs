using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class SparepartModel
    {
        [DisplayName("ID")]
        public string id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string name { get; set; }

        [DisplayName("Function")]
        [Required]
        public string function { get; set; }

        [DisplayName("Stock")]
        [Required]
        public string Stock { get; set; }

        [DisplayName("Unit")]
        [Required]
        public string unit { get; set; }

        [DisplayName("Price")]
        [Required]
        public string price { get; set; }

        public string status { get; set; }

        [DisplayName("Browse Photo")]
        public HttpPostedFileBase[] photos { get; set; }

        public string quantity { get; set; }
    }
}