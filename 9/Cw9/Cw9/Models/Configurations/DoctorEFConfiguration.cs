using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw9.Models.Configurations
{
    public class DoctorEFConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> entity)
        {
            entity.HasKey(e => e.IdDoctor);
            entity.Property(e => e.IdDoctor).UseIdentityColumn();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.ToTable("Doctor");

            entity.HasData(
                new Doctor
                { 
                    IdDoctor = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Email = "jk@wp.pl",
                },
                new Doctor
                { 
                    IdDoctor = 2,
                    FirstName = "Wiktoria",
                    LastName = "Nowak",
                    Email = "wn@wp.pl",
                },
                new Doctor
                { 
                    IdDoctor = 3,
                    FirstName = "Alina",
                    LastName = "Krawczyk",
                    Email = "ak@wp.pl",
                }
                );
        }
    }
}
