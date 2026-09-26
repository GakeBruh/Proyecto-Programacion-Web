using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HabitForge.Application.Interfaces;
using HabitForge.Infrastructure.Data;
using HabitForge.Infrastructure.Repositories;

namespace HabitForge.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {
        
        services.AddDbContext<HabitForgeDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IHabitoRepository, HabitoRepository>();

        return services;
    }
}