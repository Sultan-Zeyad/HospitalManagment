using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "المريض مطلوب")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        [Required(ErrorMessage = "الطبيب مطلوب")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "تاريخ الموعد مطلوب")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "الحالة مطلوبة")]
        public string Status { get; set; } = "منتظر";

        public string? Notes { get; set; }
    }
}
