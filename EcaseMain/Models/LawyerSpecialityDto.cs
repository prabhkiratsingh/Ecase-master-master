using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class LawyerSpecialityDto
    {
        public string Location { set; get; }
        public int Id { get; set; }
        public bool Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpecalityName { get; set; }
    }
}