using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw9.Models.Configurations
{
    public class PatientEFConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> entity)
        {
            entity.HasKey(e => e.IdPatient);
            entity.Property(e => e.IdPatient).UseIdentityColumn();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Birthdate).IsRequired();
            entity.ToTable("Patient");

            entity.HasData(
                new Patient { IdPatient = 1, FirstName = "Krzysztof", LastName = "Krawczyk", Birthdate = DateTime.Parse("1999-05-05")},
                new Patient { IdPatient = 2, FirstName = "Tadeusz", LastName = "Norek", Birthdate = DateTime.Parse("1999-01-05")},
                new Patient { IdPatient = 3, FirstName = "Daria", LastName = "Dziwisz", Birthdate = DateTime.Parse("2000-05-05")}
                );
        }
    }
}
