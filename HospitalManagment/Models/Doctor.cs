using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الطبيب مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يتجاوز 100 حرف")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "التخصص مطلوب")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "البريد الإلكتروني غير صحيح")]
        public string Email { get; set; }
    }
}
