using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class UserModel
    {
        [DisplayName("Employees Number")]
        public string nik { get; set; }

        [DisplayName("Name")]
        [Required]
        public string name { get; set; }

        [DisplayName("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DisplayName("Phone")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "email : example@mail.com")]
        public string email { get; set; }

        [DisplayName("UPT")]
        [Required]
        public int upt { get; set; }

        public string uptname { get; set; }

        [DisplayName("Role")]
        [Required]
        public string role { get; set; }
    }
}