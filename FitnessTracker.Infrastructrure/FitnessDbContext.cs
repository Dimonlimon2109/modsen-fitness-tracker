
using FitnessTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Infrastructrure
{
    public class FitnessDbContext(DbContextOptions<FitnessDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessDbContext).Assembly);
        }
    }
}
