using EventTicketingPlatform.Application.Interfaces;
using EventTicketingPlatform.Application.Services;
using EventTicketingPlatform.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventTicketingPlatform.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // AutoMapper - iba Application assembly
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

        // ===== AUTH SERVICES =====
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}