using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.UseCases.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using FitnessTracker.Tests.Base;
using FluentAssertions;
using Moq;
using Xunit;

namespace FitnessTracker.Tests.UseCases.Workouts
{
    public class UpdateWorkoutUseCaseTests : TestBase
    {
        private readonly UpdateWorkoutUseCase _useCase;

        public UpdateWorkoutUseCaseTests()
        {
            _useCase = new UpdateWorkoutUseCase(
                MockWorkoutRepository.Object,
                MockUserRepository.Object,
                MockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldUpdateWorkout()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var workoutId = 1;
            var existingWorkout = CreateTestWorkout(workoutId, user.Id);
            var request = new UpdateWorkoutRequest(
                workoutId,
                "Updated Workout",
                WorkoutType.Cardio,
                CreateTestExercises(),
                TimeSpan.FromMinutes(60),
                400,
                DateTime.UtcNow.AddDays(1));

            var mappedExercises = new List<Exercise>
            {
                new Exercise
                {
                    Name = "Updated Exercise",
                    Sets = new List<Set>
                    {
                        new Set { Reps = 12, Weight = 50.0 }
                    }
                }
            };

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingWorkout);

            MockMapper
                .Setup(x => x.Map<Exercise>(It.IsAny<ExerciseDTO>()))
                .Returns((ExerciseDTO dto) => mappedExercises.First());

            MockWorkoutRepository
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            existingWorkout.Title.Should().Be("Updated Workout");
            existingWorkout.Type.Should().Be(WorkoutType.Cardio);
            existingWorkout.Duration.Should().Be(TimeSpan.FromMinutes(60));
            existingWorkout.CaloriesBurned.Should().Be(400);
            existingWorkout.WorkoutDate.Should().Be(request.WorkoutDate);

            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.Update(existingWorkout), Times.Once);
            MockWorkoutRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
} 