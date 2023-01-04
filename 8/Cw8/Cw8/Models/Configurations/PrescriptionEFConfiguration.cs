using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw8.Models.Configurations
{
    public class PrescriptionEFConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.IdPrescription);
            builder.Property(e => e.IdPrescription).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.DueDate).IsRequired();
            builder.ToTable("Prescription");

            builder.HasOne(e => e.IdDoctorNavigation)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.IdPatientNavigation)
                .WithMany(e => e.Prescriptions)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasData(
                new Prescription { IdDoctor = 1, IdPatient = 1, IdPrescription = 1, Date = DateTime.Parse("2022-12-20"), DueDate = DateTime.Parse("2023-01-31")},
                new Prescription { IdDoctor = 2, IdPatient = 3, IdPrescription = 2, Date = DateTime.Parse("2022-12-20"), DueDate = DateTime.Parse("2023-01-31")},
                new Prescription { IdDoctor = 3, IdPatient = 2, IdPrescription = 3, Date = DateTime.Parse("2022-12-20"), DueDate = DateTime.Parse("2023-01-31")}
                );

        }
    }
}
