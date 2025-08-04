using FitnessTracker.Application.Exceptions.Auth;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.UseCases.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Tests.Base;
using FluentAssertions;
using Moq;
using Xunit;

namespace FitnessTracker.Tests.UseCases.Workouts
{
    public class DeleteWorkoutUseCaseTests : TestBase
    {
        private readonly DeleteWorkoutUseCase _useCase;

        public DeleteWorkoutUseCaseTests()
        {
            _useCase = new DeleteWorkoutUseCase(
                MockWorkoutRepository.Object,
                MockUserRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidId_ShouldDeleteWorkout()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var workoutId = 1;
            var workout = CreateTestWorkout(workoutId, user.Id);

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workout);

            MockWorkoutRepository
                .Setup(x => x.DeleteAsync(workoutId, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            MockWorkoutRepository
                .Setup(x => x.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.ExecuteAsync(workoutId, userEmail);

            // Assert
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.DeleteAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithNonExistentUser_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var userEmail = "nonexistent@example.com";
            var workoutId = 1;

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserEntity?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(
                () => _useCase.ExecuteAsync(workoutId, userEmail));

            exception.Message.Should().Be("Пользователь не найден");
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
            MockWorkoutRepository.Verify(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_WithNonExistentWorkout_ShouldThrowWorkoutNotFoundException()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var workoutId = 999;

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((WorkoutEntity?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<WorkoutNotFoundException>(
                () => _useCase.ExecuteAsync(workoutId, userEmail));

            exception.Message.Should().Be("Тренировка не найдена");
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_WithWorkoutOfAnotherUser_ShouldThrowWorkoutForbiddenException()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var workoutId = 1;
            var workout = CreateTestWorkout(workoutId, 2);

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workout);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<WorkoutForbiddenException>(
                () => _useCase.ExecuteAsync(workoutId, userEmail));

            exception.Message.Should().Be("Тренировка другого пользователя недоступна");
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.DeleteAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
} 