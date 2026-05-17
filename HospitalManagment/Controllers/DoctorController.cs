using HospitalManagment.Data;
using HospitalManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }

        // عرض قائمة الأطباء
        public IActionResult Index(string search, string specialization)
        {
            var doctors = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                doctors = doctors.Where(d =>
                    d.FullName.Contains(search) ||
                    d.Email.Contains(search));

            if (!string.IsNullOrEmpty(specialization))
                doctors = doctors.Where(d => d.Specialization == specialization);

            ViewBag.Search = search;
            ViewBag.Specialization = specialization;
            ViewBag.Specializations = _context.Doctors
                .Select(d => d.Specialization)
                .Distinct()
                .ToList();

            return View(doctors.ToList());
        }

        // فتح صفحة إضافة طبيب
        public IActionResult Create()
        {
            return View();
        }

        // حفظ الطبيب الجديد
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // فتح صفحة التعديل
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        // حفظ التعديل
        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Update(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // حذف الطبيب
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}