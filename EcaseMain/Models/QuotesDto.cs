using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class QuotesDto
    {
        public int id { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string duration { get; set; }
        public int lawyerId { get; set; }
        public int caseId { get; set; }


    }
}