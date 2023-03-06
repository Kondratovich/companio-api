using System.Text;
using Companio.AutoMapper;
using Companio.Models.Enums;
using Companio.Mongo;
using Companio.Security;
using Companio.Services;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Companio;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }
    
    // Add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<MongoSettings>(Configuration.GetSection("MongoSettings"));
        services.AddSingleton<MongoContext>();

        var jwtSettings = new JwtSettings();
        Configuration.Bind(nameof(jwtSettings),jwtSettings);
        services.AddSingleton(jwtSettings);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy =>
            {
                policy.RequireRole(Role.Administrator.ToString());
            });
            options.AddPolicy("Manager", policy =>
            {
                policy.RequireRole(Role.Manager.ToString());
            });
            options.AddPolicy("Employee", policy =>
            {
                policy.RequireRole(Role.Employee.ToString());
            });
        });

        services.AddAutoMapper(typeof(AppMappingProfile));
        services.AddMvc();

        services.AddSwaggerGen(x => 
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Companio REST API", Version = "v1"});

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Fill in your acquired bearer token here, must be like 'Bearer TOKEN_HERE'",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "Bearer <token>"
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
        });

        services.AddSingleton<MongoContext>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ICustomerService, CustomerService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Companio REST API v1"));

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "Default",
                "{controller}/{action}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}