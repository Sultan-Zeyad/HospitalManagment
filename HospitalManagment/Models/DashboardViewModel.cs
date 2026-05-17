using HospitalManagment.Models;

namespace HospitalManagement.Models
{
    public class DashboardViewModel
    {
        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalAppointments { get; set; }
        public int TodayAppointments { get; set; }
        public int PendingAppointments { get; set; }
        public int CompletedAppointments { get; set; }
        public int CancelledAppointments { get; set; }
        public List<Appointment> LatestAppointments { get; set; }
        public Dictionary<int, int> AppointmentsPerMonth { get; set; }
    }
}