using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMMS.Models
{
    public class CallendarModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}