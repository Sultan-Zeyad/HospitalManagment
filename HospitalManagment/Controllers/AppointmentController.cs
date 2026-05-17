using HospitalManagment.Data;
using HospitalManagment.Models;
using HospitalManagment.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment.Controllers
{
    [Authorize(Roles = "Admin,Receptionist,Doctor")]
    public class AppointmentController : Controller
    {
        private readonly HospitalDbContext _context;
        private readonly PdfService _pdfService;

        public AppointmentController(HospitalDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // عرض قائمة المواعيد
        public IActionResult Index(string search, string status, string date)
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                appointments = appointments.Where(a =>
                    a.Patient.FullName.Contains(search) ||
                    a.Doctor.FullName.Contains(search));

            if (!string.IsNullOrEmpty(status))
                appointments = appointments.Where(a => a.Status == status);

            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out var parsedDate))
                appointments = appointments.Where(a => a.AppointmentDate.Date == parsedDate.Date);

            ViewBag.Search = search;
            ViewBag.Status = status;
            ViewBag.Date = date;

            return View(appointments.OrderByDescending(a => a.AppointmentDate).ToList());
        }

        // فتح صفحة إضافة موعد
        public IActionResult Create()
        {
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View();
        }

        // حفظ الموعد الجديد
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View(appointment);
        }

        // فتح صفحة التعديل
        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View(appointment);
        }

        // حفظ التعديل
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View(appointment);
        }

        // حذف الموعد
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult ExportPdf(string status, string date)
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
                appointments = appointments.Where(a => a.Status == status);

            if (!string.IsNullOrEmpty(date) && DateTime.TryParse(date, out var parsedDate))
                appointments = appointments.Where(a => a.AppointmentDate.Date == parsedDate.Date);

            var pdf = _pdfService.GenerateAppointmentsPdf(
                appointments.OrderByDescending(a => a.AppointmentDate).ToList());

            return File(pdf, "application/pdf", $"appointments_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}