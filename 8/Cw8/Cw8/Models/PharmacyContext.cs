using Cw8.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Cw8.Models
{
    public class PharmacyContext : DbContext
    {
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Medicament> Medicaments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public PharmacyContext()
        { }

        public PharmacyContext(DbContextOptions<PharmacyContext> opt):base(opt)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DoctorEFConfiguration).GetTypeInfo().Assembly);
        }
    }
}
