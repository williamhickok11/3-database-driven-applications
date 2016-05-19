using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Portal.Models
{
    public class PatientProfile
    {
        public int IdPatientProfile { get; set; }
        public string PatientName { get; set; }
        public string Address { get; set; }
        public string PaymentType { get; set; }
    }
}