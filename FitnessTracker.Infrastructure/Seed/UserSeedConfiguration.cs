using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessTracker.Infrastructure.Seed
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasData(
                new UserEntity
                {
                    Id = 1,
                    Email = "user1@gmail.com",
                    PasswordHash = "$2a$11$/HwV7ghcW26LoVIoqtepoOJykEiAW7orB4MjD6MtCDRMUX8IiFQGG",
                    CreatedAt = DateTime.SpecifyKind(DateTime.Parse("2025-08-01 19:25:00"),
                    DateTimeKind.Utc)
                },
                new UserEntity
                {
                    Id = 2,
                    Email = "user@gmail.com",
                    PasswordHash = "$2a$11$/HwV7ghcW26LoVIoqtepoOJykEiAW7orB4MjD6MtCDRMUX8IiFQGG",
                    CreatedAt = DateTime.SpecifyKind(DateTime.Parse("2025-08-01T19:27:00"),
                    DateTimeKind.Utc)
                });
        }
    }
}
