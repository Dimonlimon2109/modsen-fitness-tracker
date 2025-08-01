
using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessTracker.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.RefreshToken)
                .HasMaxLength(256);
        }
    }
}
