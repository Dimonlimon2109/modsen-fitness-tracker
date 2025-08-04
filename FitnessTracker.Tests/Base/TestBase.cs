using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using FitnessTracker.Domain.Interfaces.Repositories;
using Moq;

namespace FitnessTracker.Tests.Base
{
    public abstract class TestBase
    {
        protected readonly Mock<IWorkoutRepository> MockWorkoutRepository;
        protected readonly Mock<IUserRepository> MockUserRepository;
        protected readonly Mock<IMapper> MockMapper;

        protected TestBase()
        {
            MockWorkoutRepository = new Mock<IWorkoutRepository>();
            MockUserRepository = new Mock<IUserRepository>();
            MockMapper = new Mock<IMapper>();
        }

        protected UserEntity CreateTestUser(int id = 1, string email = "test@example.com")
        {
            return new UserEntity
            {
                Id = id,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };
        }

        protected WorkoutEntity CreateTestWorkout(int id = 1, int userId = 1)
        {
            return new WorkoutEntity
            {
                Id = id,
                UserId = userId,
                Title = "Test Workout",
                Type = WorkoutType.Strength,
                Duration = TimeSpan.FromMinutes(45),
                CaloriesBurned = 300,
                WorkoutDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Exercises = new List<Exercise>
                {
                    new Exercise
                    {
                        Name = "Bench Press",
                        Sets = new List<Set>
                        {
                            new Set { Reps = 10, Weight = 60.0 },
                            new Set { Reps = 8, Weight = 70.0 }
                        }
                    }
                }
            };
        }

        protected WorkoutDTO CreateTestWorkoutDTO(int id = 1)
        {
            return new WorkoutDTO
            {
                Id = id,
                Title = "Test Workout",
                WorkoutType = WorkoutType.Strength.ToString(),
                Duration = TimeSpan.FromMinutes(45),
                CaloriesBurned = 300,
                WorkoutDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Exercises = new List<ExerciseDTO>
                {
                    new ExerciseDTO
                    {
                        Name = "Bench Press",
                        Sets = new List<SetDTO>
                        {
                            new SetDTO { Reps = 10, Weight = 60.0 },
                            new SetDTO { Reps = 8, Weight = 70.0 }
                        }
                    }
                }
            };
        }

        protected List<ExerciseDTO> CreateTestExercises()
        {
            return new List<ExerciseDTO>
            {
                new ExerciseDTO
                {
                    Name = "Bench Press",
                    Sets = new List<SetDTO>
                    {
                        new SetDTO { Reps = 10, Weight = 60.0 },
                        new SetDTO { Reps = 8, Weight = 70.0 }
                    }
                }
            };
        }
    }
} 