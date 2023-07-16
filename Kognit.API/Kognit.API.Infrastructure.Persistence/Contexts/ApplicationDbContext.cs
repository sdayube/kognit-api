using Kognit.API.Application.Interfaces;
using Kognit.API.Domain.Common;
using Kognit.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Kognit.API.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly ILoggerFactory _loggerFactory;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTimeService dateTime,
            ILoggerFactory loggerFactory
            ) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _loggerFactory = loggerFactory;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var _mockService = Database.GetService<IMockService>();
            var seedUsers = _mockService.SeedUsers(1000);

            builder.Entity<User>().HasData(seedUsers);

            builder.Entity<BaseEntity>()
                .Property(u => u.Created)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Entity<BaseEntity>()
                .Property(u => u.LastModified)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();


            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}