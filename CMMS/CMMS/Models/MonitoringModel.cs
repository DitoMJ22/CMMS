using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class MonitoringModel
    {
        [DisplayName("ID")]
        public string id_monitoring { get; set; }

        [DisplayName("Temperature")]
        [Required]
        public string temperature { get; set; }

        [DisplayName("Nama Machine")]
        [Required]
        public string id_machine { get; set; }

        [DisplayName("Nama Sensor")]
        [Required]
        public string nama_sensor { get; set; }

        [DisplayName("Waktu")]
        [Required]
        public string waktu { get; set; }
    }
}