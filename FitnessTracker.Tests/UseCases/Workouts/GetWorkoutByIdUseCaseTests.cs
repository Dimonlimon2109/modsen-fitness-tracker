using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
using FitnessTracker.Application.Exceptions.Workouts;
using FitnessTracker.Application.UseCases.Workouts;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Tests.Base;
using FluentAssertions;
using Moq;
using Xunit;

namespace FitnessTracker.Tests.UseCases.Workouts
{
    public class GetWorkoutByIdUseCaseTests : TestBase
    {
        private readonly GetWorkoutByIdUseCase _useCase;

        public GetWorkoutByIdUseCaseTests()
        {
            _useCase = new GetWorkoutByIdUseCase(
                MockWorkoutRepository.Object,
                MockUserRepository.Object,
                MockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidId_ShouldReturnWorkout()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var workoutId = 1;
            var workout = CreateTestWorkout(workoutId, user.Id);
            var workoutDTO = CreateTestWorkoutDTO(workoutId);

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(workout);

            MockMapper
                .Setup(x => x.Map<WorkoutDTO>(workout))
                .Returns(workoutDTO);

            // Act
            var result = await _useCase.ExecuteAsync(workoutId, userEmail);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(workoutId);
            result.Title.Should().Be(workoutDTO.Title);

            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetByIdAsync(workoutId, It.IsAny<CancellationToken>()), Times.Once);
            MockMapper.Verify(x => x.Map<WorkoutDTO>(workout), Times.Once);
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
            MockMapper.Verify(x => x.Map<WorkoutDTO>(It.IsAny<WorkoutEntity>()), Times.Never);
        }
    }
} 