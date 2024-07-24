using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class NotificationModel
    {
        [DisplayName("ID")]
        public string id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string id_notification { get; set; }

        [DisplayName("UPT")]
        [Required]
        public string title { get; set; }

        public string description { get; set; }

        [DisplayName("PIC")]
        [Required]
        public string received_by { get; set; }
        public string date { get; set; }
        public string status { get; set; }
    }
}