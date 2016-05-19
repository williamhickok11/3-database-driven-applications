using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Portal.ViewModels
{
    public class PatientAilmentDetailViewModel
    {
        public int IdAilment { get; set; }
        public int IdPatient { get; set; }
        public string AilmentName { get; set; }
    }
}