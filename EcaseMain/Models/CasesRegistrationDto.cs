using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class CasesRegistrationDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string clientName { get; set; }
        public string specialityName { get; set; }
        public int approvedLawyerId { get; set; }


    }
}