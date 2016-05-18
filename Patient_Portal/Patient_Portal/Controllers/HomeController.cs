using Patient_Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Patient_Portal.Controllers
{
    public class HomeController : Controller
    {
        // Set up the Db context to the database
        private Patient_PortalDbContext _context;
        public HomeController(Patient_PortalDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public ActionResult Index()
        {
            // Get access to the patient info in the database
            var patients = from a in _context.PatientProfile
                          select a;

            //PatientProfile CurrentPatients = new PatientProfile();

            // Pass the patients to the view
            return View(patients.ToList());
        }
    }
}