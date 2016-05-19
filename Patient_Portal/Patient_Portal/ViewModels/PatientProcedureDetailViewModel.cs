using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Portal.ViewModels
{
    public class PatientProcedureDetailViewModel
    {
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public int ProcedurePrice { get; set; }
    }
}