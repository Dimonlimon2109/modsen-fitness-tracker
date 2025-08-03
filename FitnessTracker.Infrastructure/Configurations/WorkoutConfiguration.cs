
using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace FitnessTracker.Infrastructure.Configurations
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<WorkoutEntity>
    {
        public void Configure(EntityTypeBuilder<WorkoutEntity> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Title)
                .HasMaxLength(100);

            builder.Property(w => w.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(w => w.CreatedAt).IsRequired();
            builder.Property(w => w.Duration).IsRequired();
            builder.Property(w => w.WorkoutDate).IsRequired();

            builder.Property(w => w.ProgressPhotos)
                .HasConversion(
                v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>())
                .HasColumnType("jsonb")
                .IsRequired(false)
                .Metadata.SetValueComparer(new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));


            builder.OwnsMany(w => w.Exercises, excerciseBuilder =>
            {
                excerciseBuilder.WithOwner().HasForeignKey("WorkoutId");
                excerciseBuilder.Property<int>("Id");
                excerciseBuilder.HasKey("Id");

                excerciseBuilder.Property(e => e.Name).HasMaxLength(100);
                excerciseBuilder.OwnsMany(e => e.Sets, setBuilder =>
                {
                    setBuilder.WithOwner().HasForeignKey("ExerciseId");
                    setBuilder.Property<int>("Id");
                    setBuilder.HasKey("Id");

                    setBuilder.Property(s => s.Reps).IsRequired();
                    setBuilder.Property(s => s.Weight).IsRequired();
                });
            });


            builder.HasOne(w => w.User)
                .WithMany(u => u.Workouts)
                .HasForeignKey(w => w.UserId);
        }
    }
}
