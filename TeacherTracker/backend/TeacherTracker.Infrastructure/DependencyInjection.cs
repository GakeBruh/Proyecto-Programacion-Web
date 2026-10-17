using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TeacherTracker.Application.Interfaces;
using TeacherTracker.Infrastructure.Data;
using TeacherTracker.Infrastructure.Repositories;

namespace TeacherTracker.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
        
        services.AddDbContext<TeacherTrackerDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<ITeacherRepository, TeacherRepository>();

        return services;
    }
}