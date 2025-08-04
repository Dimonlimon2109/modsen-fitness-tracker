using FitnessTracker.Application.Contracts.Requests;
using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.UseCases.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;
using FitnessTracker.Tests.Base;
using FluentAssertions;
using Moq;
using Xunit;

namespace FitnessTracker.Tests.UseCases.Workouts
{
    public class CreateWorkoutUseCaseTests : TestBase
    {
        private readonly CreateWorkoutUseCase _useCase;

        public CreateWorkoutUseCaseTests()
        {
            _useCase = new CreateWorkoutUseCase(
                MockWorkoutRepository.Object,
                MockUserRepository.Object,
                MockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldCreateWorkout()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var request = new CreateWorkoutRequest(
                "Morning Workout",
                WorkoutType.Strength,
                CreateTestExercises(),
                TimeSpan.FromMinutes(45),
                300,
                DateTime.UtcNow);

            var workoutEntity = CreateTestWorkout();
            workoutEntity.UserId = user.Id;

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockMapper
                .Setup(x => x.Map<WorkoutEntity>(request))
                .Returns(workoutEntity);

            MockWorkoutRepository
                .Setup(x => x.AddAsync(It.IsAny<WorkoutEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            MockWorkoutRepository
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockMapper.Verify(x => x.Map<WorkoutEntity>(request), Times.Once);
            MockWorkoutRepository.Verify(x => x.AddAsync(It.IsAny<WorkoutEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithNonExistentUser_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var userEmail = "nonexistent@example.com";
            var request = new CreateWorkoutRequest(
                "Morning Workout",
                WorkoutType.Strength,
                CreateTestExercises(),
                TimeSpan.FromMinutes(45),
                300,
                DateTime.UtcNow);

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserEntity?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(
                () => _useCase.ExecuteAsync(request, userEmail));

            exception.Message.Should().Be("Пользователь не найден");
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockMapper.Verify(x => x.Map<WorkoutEntity>(It.IsAny<CreateWorkoutRequest>()), Times.Never);
            MockWorkoutRepository.Verify(x => x.AddAsync(It.IsAny<WorkoutEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldSetUserIdFromUser()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var request = new CreateWorkoutRequest(
                "Morning Workout",
                WorkoutType.Strength,
                CreateTestExercises(),
                TimeSpan.FromMinutes(45),
                300,
                DateTime.UtcNow);

            var workoutEntity = CreateTestWorkout();
            workoutEntity.UserId = 0;

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockMapper
                .Setup(x => x.Map<WorkoutEntity>(request))
                .Returns(workoutEntity);

            MockWorkoutRepository
                .Setup(x => x.AddAsync(It.IsAny<WorkoutEntity>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            MockWorkoutRepository
                .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            workoutEntity.UserId.Should().Be(user.Id);
        }
    }
} 