using Companio.AutoMapper;
using Companio.Configs.Security;
using Companio.Data;
using Companio.Models.Enums;
using Companio.Services;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Companio.Startup;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Configuration
        var jwtSettings = new JwtSettings();
        configuration.Bind(nameof(jwtSettings), jwtSettings);
        var connectionString = configuration.GetConnectionString("Postgre");

        // Register services
        services.AddDbContext(connectionString);
        services.AddJwtAuthentication(jwtSettings);
        services.AddAuthorizationPolicies();
        services.AddAutoMapper(typeof(AppMappingProfile));
        services.AddControllers();
        services.AddCorsPolicy();
        services.AddApplicationServices();
        services.AddOpenApi();
    }

    private static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
             options.AddDocumentTransformer((document, _, _) =>
             {
                 var scheme = new OpenApiSecurityScheme()
                 {
                     BearerFormat = "JSON Web Token",
                     Description = "Bearer authentication using a JWT.",
                     Scheme = "bearer",
                     Type = SecuritySchemeType.Http,
                     Reference = new()
                     {
                         Id = "Bearer",
                         Type = ReferenceType.SecurityScheme,
                     },
                 };

                 document.Components ??= new();
                 document.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();
                 document.Components.SecuritySchemes[scheme.Reference.Id] = scheme;

                 return Task.CompletedTask;
             })
        );

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
            };
        });

        return services;
    }

    private static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Administrator", policy => policy.RequireRole(Role.Administrator.ToString()))
            .AddPolicy("Manager", policy => policy.RequireRole(Role.Manager.ToString()))
            .AddPolicy("Employee", policy => policy.RequireRole(Role.Employee.ToString()));

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<IAbsenceTimelineService, AbsenceTimelineService>();

        return services;
    }

    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        return services;
    }
}