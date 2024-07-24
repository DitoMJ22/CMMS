using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class UserAPIModel
    {
        [DisplayName("result")]
        public string result { get; set; }

        [DisplayName("NPK")]
        public string npk { get; set; }

        [DisplayName("username")]
        [Required]
        public string username { get; set; }

        [DisplayName("nama")]
        [Required]
        public string nama { get; set; }

        [DisplayName("Email")]
        [Required]
        //[DataType(DataType.EmailAddress, ErrorMessage = "email : example@mail.com")]
        public string email { get; set; }

        [DisplayName("struktur")]
        [Required]
        public string struktur { get; set; }

        [DisplayName("jabatan")]
        [Required]
        public string jabatan { get; set; }

        [DisplayName("role")]
        [Required]
        public string role { get; set; }
    }
}