using BlackTitanium.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackTitanium;

public class DatabaseContext : DbContext {
    private readonly string _connectionString;

    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupAttendance> GroupAttendances { get; set; } = null!;
    public DbSet<PersonalAttendance> PersonalAttendances { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public DatabaseContext(IConfiguration configuration) {
        var connectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString");
        if (connectionString is null) {
            throw new Exception("Settings key Database.ConnectionString cannot be empty");
        }

        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
    }
}