using HospitalManagement.Models;
using HospitalManagment.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly HospitalDbContext _context;
        public HomeController(HospitalDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            var model = new DashboardViewModel
            {
                TotalPatients = _context.Patients.Count(),
                TotalDoctors = _context.Doctors.Count(),
                TotalAppointments = _context.Appointments.Count(),
                TodayAppointments = _context.Appointments
                                      .Count(a => a.AppointmentDate.Date == today),
                PendingAppointments = _context.Appointments
                                          .Count(a => a.Status == "منتظر"),
                CompletedAppointments = _context.Appointments
                                          .Count(a => a.Status == "مكتمل"),
                CancelledAppointments = _context.Appointments
                                          .Count(a => a.Status == "ملغي"),
                LatestAppointments = _context.Appointments
                                       .Include(a => a.Patient)
                                       .Include(a => a.Doctor)
                                       .OrderByDescending(a => a.AppointmentDate)
                                       .Take(5)
                                       .ToList(),
                AppointmentsPerMonth = _context.Appointments
                                         .GroupBy(a => a.AppointmentDate.Month)
                                         .Select(g => new { Month = g.Key, Count = g.Count() })
                                         .OrderBy(g => g.Month)
                                         .ToDictionary(g => g.Month, g => g.Count)
            };

            return View(model);
        }
    }
}