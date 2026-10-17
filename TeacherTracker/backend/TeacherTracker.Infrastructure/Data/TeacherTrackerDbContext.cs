using Microsoft.EntityFrameworkCore;
using TeacherTracker.Domain.Entities;

namespace TeacherTracker.Infrastructure.Data;

public class TeacherTrackerDbContext : DbContext {
    public TeacherTrackerDbContext(DbContextOptions<TeacherTrackerDbContext> options) : base(options) { }
    
    public DbSet<TeacherGlobal> TeachersGlobales { get; set; }
}