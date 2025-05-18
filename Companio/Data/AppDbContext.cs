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
}
