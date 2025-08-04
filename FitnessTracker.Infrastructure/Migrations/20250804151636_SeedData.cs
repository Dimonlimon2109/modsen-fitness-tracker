using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 1, 19, 25, 0, 0, DateTimeKind.Utc), "user1@gmail.com", "$2a$11$/HwV7ghcW26LoVIoqtepoOJykEiAW7orB4MjD6MtCDRMUX8IiFQGG", null, null },
                    { 2, new DateTime(2025, 8, 1, 19, 27, 0, 0, DateTimeKind.Utc), "user@gmail.com", "$2a$11$/HwV7ghcW26LoVIoqtepoOJykEiAW7orB4MjD6MtCDRMUX8IiFQGG", null, null }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "CaloriesBurned", "CreatedAt", "Duration", "ProgressPhotos", "Title", "Type", "UserId", "WorkoutDate" },
                values: new object[,]
                {
                    { 1, 320, new DateTime(2025, 8, 1, 19, 28, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 0, 45, 0, 0), "[]", "Morning Strength Training", 0, 1, new DateTime(2025, 8, 10, 19, 30, 0, 0, DateTimeKind.Utc) },
                    { 2, 250, new DateTime(2025, 8, 1, 19, 29, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 0, 30, 0, 0), "[]", "Evening Cardio Session", 1, 1, new DateTime(2025, 8, 10, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 400, new DateTime(2025, 8, 1, 19, 30, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 1, 0, 0, 0), "[]", "Full Body Workout", 0, 2, new DateTime(2025, 8, 10, 17, 30, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Name", "WorkoutId" },
                values: new object[,]
                {
                    { 1, "Bench Press", 1 },
                    { 2, "Squats", 1 },
                    { 3, "Running", 2 },
                    { 4, "Deadlifts", 3 },
                    { 5, "Pull-ups", 3 }
                });

            migrationBuilder.InsertData(
                table: "Set",
                columns: new[] { "Id", "ExerciseId", "Reps", "Weight" },
                values: new object[,]
                {
                    { 1, 1, 10, 60.0 },
                    { 2, 1, 8, 70.0 },
                    { 3, 1, 6, 80.0 },
                    { 4, 2, 12, 50.0 },
                    { 5, 2, 10, 60.0 },
                    { 6, 2, 8, 70.0 },
                    { 7, 3, 1, 0.0 },
                    { 8, 4, 8, 80.0 },
                    { 9, 4, 6, 90.0 },
                    { 10, 4, 4, 100.0 },
                    { 11, 5, 8, 0.0 },
                    { 12, 5, 6, 0.0 },
                    { 13, 5, 5, 0.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Set",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercise",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
