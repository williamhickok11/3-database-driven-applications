using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patient_Portal.Models
{
    public class Procedure
    {
        public int IdProcedure { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureDescription { get; set; }
        public int ProcedurePrice { get; set; }
    }
}