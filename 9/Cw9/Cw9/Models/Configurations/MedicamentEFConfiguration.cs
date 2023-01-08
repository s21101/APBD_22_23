using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw9.Models.Configurations
{
    public class MedicamentEFConfiguration : IEntityTypeConfiguration<Medicament>
    {
        public void Configure(EntityTypeBuilder<Medicament> builder)
        {
            builder.HasKey(e => e.IdMedicament);
            builder.Property(e => e.IdMedicament).IsRequired().UseIdentityColumn();
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
            builder.ToTable("Medicament");

            builder.HasData(
                new Medicament { IdMedicament = 1, Name = "Med 1", Description = "Med 1", Type = "aa"},
                new Medicament { IdMedicament = 2, Name = "Med 2", Description = "Med 2", Type = "aa"},
                new Medicament { IdMedicament = 3, Name = "Med 3", Description = "Med 3", Type = "bb"}
                );
        }
    }
}
