
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessTracker.Infrastructure.Seed
{
    public class WorkoutSeedConfiguration : IEntityTypeConfiguration<WorkoutEntity>
    {
        public void Configure(EntityTypeBuilder<WorkoutEntity> builder)
        {
            builder.HasData(
                new WorkoutEntity
                {
                    Id = 1,
                    UserId = 1,
                    Title = "Morning Strength Training",
                    Type = WorkoutType.Strength,
                    Duration = TimeSpan.FromMinutes(45),
                    CaloriesBurned = 320,
                    WorkoutDate = DateTime.SpecifyKind(DateTime.Parse("2025-08-10T19:30:00"),
                    DateTimeKind.Utc),
                    CreatedAt = DateTime.SpecifyKind(DateTime.Parse("2025-08-01T19:28:00"),
                    DateTimeKind.Utc)
                },
                new WorkoutEntity
                {
                    Id = 2,
                    UserId = 1,
                    Title = "Evening Cardio Session",
                    Type = WorkoutType.Cardio,
                    Duration = TimeSpan.FromMinutes(30),
                    CaloriesBurned = 250,
                    WorkoutDate = DateTime.SpecifyKind(DateTime.Parse("2025-08-10T18:00:00"),
                    DateTimeKind.Utc),
                    CreatedAt = DateTime.SpecifyKind(DateTime.Parse("2025-08-01T19:29:00"),
                    DateTimeKind.Utc)
                },
                new WorkoutEntity
                {
                    Id = 3,
                    UserId = 2,
                    Title = "Full Body Workout",
                    Type = WorkoutType.Strength,
                    Duration = TimeSpan.FromHours(1),
                    CaloriesBurned = 400,
                    WorkoutDate = DateTime.SpecifyKind(DateTime.Parse("2025-08-10T17:30:00"),
                    DateTimeKind.Utc),
                    CreatedAt = DateTime.SpecifyKind(DateTime.Parse("2025-08-01T19:30:00"),
                    DateTimeKind.Utc)
                }
            );

            builder.OwnsMany(w => w.Exercises).HasData(
                new { WorkoutId = 1, Id = 1, Name = "Bench Press" },
                new { WorkoutId = 1, Id = 2, Name = "Squats" },
                new { WorkoutId = 2, Id = 3, Name = "Running" },
                new { WorkoutId = 3, Id = 4, Name = "Deadlifts" },
                new { WorkoutId = 3, Id = 5, Name = "Pull-ups" }
            );

            builder.OwnsMany(w => w.Exercises).OwnsMany(e => e.Sets).HasData(
                new { ExerciseId = 1, Id = 1, Reps = 10, Weight = 60.0 },
                new { ExerciseId = 1, Id = 2, Reps = 8, Weight = 70.0 },
                new { ExerciseId = 1, Id = 3, Reps = 6, Weight = 80.0 },

                new { ExerciseId = 2, Id = 4, Reps = 12, Weight = 50.0 },
                new { ExerciseId = 2, Id = 5, Reps = 10, Weight = 60.0 },
                new { ExerciseId = 2, Id = 6, Reps = 8, Weight = 70.0 },

                new { ExerciseId = 3, Id = 7, Reps = 1, Weight = 0.0 },

                new { ExerciseId = 4, Id = 8, Reps = 8, Weight = 80.0 },
                new { ExerciseId = 4, Id = 9, Reps = 6, Weight = 90.0 },
                new { ExerciseId = 4, Id = 10, Reps = 4, Weight = 100.0 },

                new { ExerciseId = 5, Id = 11, Reps = 8, Weight = 0.0 },
                new { ExerciseId = 5, Id = 12, Reps = 6, Weight = 0.0 },
                new { ExerciseId = 5, Id = 13, Reps = 5, Weight = 0.0 }
            );
        }
    }
}
