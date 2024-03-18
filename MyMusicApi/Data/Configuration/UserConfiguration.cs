using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusicApi.Core.Models;

namespace MyMusicApi.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .Property(x=>x.Id)
                .UseIdentityColumn();
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .ToTable("Users");
        }
    }
}
