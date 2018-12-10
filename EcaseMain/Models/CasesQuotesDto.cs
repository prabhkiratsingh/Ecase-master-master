using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class CasesQuotesDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string specialityName { get; set; }
        public int approvedLaywerId { get; set; }
        public string LawyerName { get;  set; }
        public List<Quoted> Quoteds { get; set; }


    }

    public class Quoted
    {
        public int id { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string duration { get; set; }
        public int lawyerid { get; set; }
        public string lawyername { get; set; }
        public string emailId { get; set; }
    }
}