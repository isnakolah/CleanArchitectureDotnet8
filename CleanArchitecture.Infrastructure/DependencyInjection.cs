using CleanArchitecture.Application.Abstractions.Idempotency;
using CleanArchitecture.Application.Data;
using CleanArchitecture.Application.FeatureFlags;
using CleanArchitecture.Infrastructure.FeatureFlags;
using CleanArchitecture.Infrastructure.Idempotency;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(opt => opt.UseInMemoryDatabase("in-memory"));

        services.AddMemoryCache();

        services.AddScoped<IIdempotencyService, IdempotencyService>();
        services.AddScoped<IFeatureFlagService, FeatureFlagService>();

        services.AddTransient<IFeatureFilter, DatabaseFeatureFilter>();
        services.AddFeatureManagement();

        return services;
    }
}