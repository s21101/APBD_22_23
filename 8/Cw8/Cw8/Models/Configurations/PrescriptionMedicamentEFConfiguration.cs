using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw8.Models.Configurations
{
    public class PrescriptionMedicamentEFConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.HasKey(e => new { e.IdMedicament, e.IdPrescription });
            builder.Property(e =>e.Details).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.IdMedicamentNavigation)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.IdPrescriptionsNavigation)
                .WithMany(e => e.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.ToTable("Prescription_Medicament");

            builder.HasData(
                new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 2, Details = "det"},
                new PrescriptionMedicament { IdPrescription = 2, IdMedicament = 3, Dose = 1, Details = "det"},
                new PrescriptionMedicament { IdPrescription = 3, IdMedicament = 2, Dose = 5, Details = "det"}
                );
        }
    }
}
