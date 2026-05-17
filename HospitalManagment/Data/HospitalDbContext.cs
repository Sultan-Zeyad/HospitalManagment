using Microsoft.EntityFrameworkCore;
using HospitalManagment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace HospitalManagment.Data
{
    public class HospitalDbContext : IdentityDbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients {get; set;}
        public DbSet<Doctor> Doctors {get; set;}
        public DbSet<Appointment> Appointments {get; set;}
    }
}
