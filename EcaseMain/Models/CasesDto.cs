using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class CasesDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int clientid { get; set; }
        public int specialityid { get; set; }
        public int approvedlawyerid { get; set; }


    }
}