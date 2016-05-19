using Patient_Portal.Models;
using Patient_Portal.ViewModels;
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
        private Patient_PortalDbContext _context = new Patient_PortalDbContext();

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

        // Detail View that accepts the id for the patient in the argument
        public ActionResult Details(int patientId)
        {
            // Get a list of all the ailments on the selected patient
            var patientAilments = (from a in _context.Ailment
                                   join ap in _context.AilmentPerscription
                                   on a.IdAilment equals ap.IdAilment

                                   // Filter data by the patient clicked on
                                   where ap.IdPatientProfile == patientId

                                   select new PatientAilmentDetailViewModel
                                   {
                                       AilmentName = a.AilmentName,
                                   }).ToList();


            // Get a list of all the Medications
            var patientMeds = (from m in _context.Medication
                                   join mp in _context.MedPerscription
                                   on m.IdMedication equals mp.IdMedication

                                   // Filter data by the patient clicked on
                                   where mp.IdPatientProfile == patientId

                                   select new PatientMedDetailVewModel
                                   {
                                        MedicationPrice = m.MedicationPrice,
                                        MedicationName = m.MedicationName,
                                        MedicationDescription = m.MedicationDescription
                                   }).ToList();

            return View(patientMeds);
            
        }
    }
}