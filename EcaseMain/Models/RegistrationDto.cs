using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcaseMain.Models
{
    public class RegistrationDto
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string location { get; set; }
        public string speciality { get; set; }
        public string certificateNo { get; set; }
        public string aboutMe { get; set;  }
        public string password { get; set; }
        public string accountType { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }


    }
}