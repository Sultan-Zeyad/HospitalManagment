using HospitalManagment.Data;
using HospitalManagment.Models;
using HospitalManagment.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment.Controllers
{
    [Authorize(Roles = "Admin,Receptionist")]
    public class PatientController : Controller
    {
        private readonly HospitalDbContext _context;
        private readonly PdfService _pdfService;
        public PatientController(HospitalDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // عرض قائمة المرضى
        public IActionResult Index(string search)
        {
            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                patients = patients.Where(p =>
                    p.FullName.Contains(search) ||
                    p.PhoneNumber.Contains(search) ||
                    p.Gender.Contains(search));

            ViewBag.Search = search;
            return View(patients.ToList());
        }

        // فتح صفحة إضافة مريض
        public IActionResult Create()
        {
            return View();
        }

        // حفظ المريض الجديد
        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
        // فتح صفحة التعديل
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // حفظ التعديل
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Update(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // حذف المريض
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ExportPdf(string search)
        {
            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                patients = patients.Where(p =>
                    p.FullName.Contains(search) ||
                    p.PhoneNumber.Contains(search));

            var pdf = _pdfService.GeneratePatientsPdf(patients.ToList());
            return File(pdf, "application/pdf", $"patients_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}