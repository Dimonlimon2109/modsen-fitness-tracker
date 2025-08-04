using AutoMapper;
using FitnessTracker.Application.Contracts.DTOs;
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
    public class GetAllWorkoutsUseCaseTests : TestBase
    {
        private readonly GetAllWorkoutsUseCase _useCase;

        public GetAllWorkoutsUseCaseTests()
        {
            _useCase = new GetAllWorkoutsUseCase(
                MockWorkoutRepository.Object,
                MockUserRepository.Object,
                MockMapper.Object);
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnWorkouts()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var request = new GetAllWorkoutsRequest(
                Title: null,
                Type: null,
                StartDate: null,
                EndDate: null,
                MinDuration: null,
                MaxDuration: null,
                Page: 1,
                PageSize: 10,
                SortBy: null,
                Order: "asc");

            var workouts = new List<WorkoutEntity>
            {
                CreateTestWorkout(1, user.Id),
                CreateTestWorkout(2, user.Id)
            };

            var workoutDTOs = new List<WorkoutDTO>
            {
                CreateTestWorkoutDTO(1),
                CreateTestWorkoutDTO(2)
            };

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetAllWorkoutsWithFiltersAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.Page,
                    request.PageSize,
                    request.SortBy,
                    request.Order,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(workouts);

            MockWorkoutRepository
                .Setup(x => x.GetTotalPagesAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            MockMapper
                .Setup(x => x.Map<WorkoutDTO>(It.IsAny<WorkoutEntity>()))
                .Returns((WorkoutEntity entity) => workoutDTOs.First(w => w.Id == entity.Id));

            // Act
            var result = await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            result.Should().NotBeNull();
            result.Workouts.Should().HaveCount(2);
            result.TotalPages.Should().Be(1);

            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetAllWorkoutsWithFiltersAsync(
                user.Id,
                request.Title,
                request.Type,
                request.StartDate,
                request.EndDate,
                request.MinDuration,
                request.MaxDuration,
                request.Page,
                request.PageSize,
                request.SortBy,
                request.Order,
                It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetTotalPagesAsync(
                user.Id,
                request.Title,
                request.Type,
                request.StartDate,
                request.EndDate,
                request.MinDuration,
                request.MaxDuration,
                request.PageSize,
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithNonExistentUser_ShouldThrowUserNotFoundException()
        {
            // Arrange
            var userEmail = "nonexistent@example.com";
            var request = new GetAllWorkoutsRequest(
                Title: null,
                Type: null,
                StartDate: null,
                EndDate: null,
                MinDuration: null,
                MaxDuration: null,
                Page: 1,
                PageSize: 10,
                SortBy: null,
                Order: "asc");

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync((UserEntity?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UserNotFoundException>(
                () => _useCase.ExecuteAsync(request, userEmail));

            exception.Message.Should().Be("Пользователь не найден");
            MockUserRepository.Verify(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()), Times.Once);
            MockWorkoutRepository.Verify(x => x.GetAllWorkoutsWithFiltersAsync(
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<WorkoutType?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string?>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_WithFilters_ShouldApplyFilters()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var request = new GetAllWorkoutsRequest(
                Title: "Morning",
                Type: WorkoutType.Strength,
                StartDate: DateTime.UtcNow.AddDays(-7),
                EndDate: DateTime.UtcNow,
                MinDuration: TimeSpan.FromMinutes(30),
                MaxDuration: TimeSpan.FromMinutes(60),
                Page: 1,
                PageSize: 10,
                SortBy: "Title",
                Order: "desc");

            var workouts = new List<WorkoutEntity>
            {
                CreateTestWorkout(1, user.Id)
            };

            var workoutDTOs = new List<WorkoutDTO>
            {
                CreateTestWorkoutDTO(1)
            };

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetAllWorkoutsWithFiltersAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.Page,
                    request.PageSize,
                    request.SortBy,
                    request.Order,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(workouts);

            MockWorkoutRepository
                .Setup(x => x.GetTotalPagesAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            MockMapper
                .Setup(x => x.Map<WorkoutDTO>(It.IsAny<WorkoutEntity>()))
                .Returns(workoutDTOs.First());

            // Act
            var result = await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            result.Should().NotBeNull();
            result.Workouts.Should().HaveCount(1);
            result.TotalPages.Should().Be(1);

            MockWorkoutRepository.Verify(x => x.GetAllWorkoutsWithFiltersAsync(
                user.Id,
                "Morning",
                WorkoutType.Strength,
                request.StartDate,
                request.EndDate,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                1,
                10,
                "Title",
                "desc",
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WithEmptyWorkouts_ShouldReturnEmptyList()
        {
            // Arrange
            var userEmail = "test@example.com";
            var user = CreateTestUser(1, userEmail);
            var request = new GetAllWorkoutsRequest(
                Title: null,
                Type: null,
                StartDate: null,
                EndDate: null,
                MinDuration: null,
                MaxDuration: null,
                Page: 1,
                PageSize: 10,
                SortBy: null,
                Order: "asc");

            MockUserRepository
                .Setup(x => x.GetUserByEmailAsync(userEmail, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            MockWorkoutRepository
                .Setup(x => x.GetAllWorkoutsWithFiltersAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.Page,
                    request.PageSize,
                    request.SortBy,
                    request.Order,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<WorkoutEntity>());

            MockWorkoutRepository
                .Setup(x => x.GetTotalPagesAsync(
                    user.Id,
                    request.Title,
                    request.Type,
                    request.StartDate,
                    request.EndDate,
                    request.MinDuration,
                    request.MaxDuration,
                    request.PageSize,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            // Act
            var result = await _useCase.ExecuteAsync(request, userEmail);

            // Assert
            result.Should().NotBeNull();
            result.Workouts.Should().BeEmpty();
            result.TotalPages.Should().Be(0);
        }
    }
} 