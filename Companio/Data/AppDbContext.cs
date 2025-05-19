using Companio.Models;
using Microsoft.EntityFrameworkCore;
using Task = Companio.Models.Task;

namespace Companio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AbsenceTimeline> AbsenceTimelines { get; set; }
    public DbSet<AuthenticationResult> AuthenticationResults { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>().HasData(
            new Team
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Dev Team",
                Description = "Handles development"
            },
            new Team
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "QA Team",
                Description = "Handles quality assurance"
            }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                PasswordHash = "admin",
                Role = 0,
                TeamId = Guid.Parse("11111111-1111-1111-1111-111111111111")
            }
        );
    }
}
