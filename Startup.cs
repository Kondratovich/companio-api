using Companio.AutoMapper;
using Companio.Models;
using Companio.Mongo;
using Companio.Services;
using Companio.Services.Interfaces;
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
        
        services.AddAutoMapper(typeof(AppMappingProfile));

        services.AddRazorPages();
        services.AddControllersWithViews();

        services.AddSwaggerGen(x => 
        {
            x.SwaggerDoc("v1", new OpenApiInfo { Title = "Companio REST API", Version = "v1"});
        });

        services.AddSingleton<MongoContext>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITeamService, TeamService>();
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

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "Default",
                "{controller}/{action}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}