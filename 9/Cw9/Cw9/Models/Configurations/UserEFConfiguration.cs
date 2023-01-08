using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cw9.Models.Configurations
{
    public class UserEFConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(e => e.Login).IsRequired().HasMaxLength(10);
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.Salt).IsRequired();

            builder.ToTable("User");

            //builder.HasData()

        }
    }
}
