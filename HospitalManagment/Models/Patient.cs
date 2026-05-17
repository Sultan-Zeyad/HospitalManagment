using System.ComponentModel.DataAnnotations;

namespace HospitalManagment.Models
{
    // This Model is a Class Represent a Information about Patient
    // (id, fullname, phonenumber, DateofBirth, Gender)
    public class Patient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "الاسم الكامل مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يتجاوز 100 حرف")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "تاريخ الميلاد مطلوب")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "الجنس مطلوب")]
        public string Gender { get; set; }
    }
}
