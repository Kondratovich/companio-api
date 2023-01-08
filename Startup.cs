using Companio.Data;
using Companio.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<DataContext>();
        services.AddControllersWithViews();

        services.AddSwaggerGen(x => 
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Companio REST API", Version = "v1"});
        });

        services.AddSingleton<IProjectService, ProjectService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
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