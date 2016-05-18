using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Portal.Models
{
    public class ProcedurePrescription
    {
        public int IdProcedurePrescription { get; set; }
        public int IdProcedure { get; set; }
        public int IdPatientProfile { get; set; }
    }
}