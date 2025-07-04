﻿using Companio.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Companio.Startup;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.UseStaticFiles();
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.MapOpenApi();
            app.MapScalarApiReference(options => options
                .WithTitle("Companio API")
                .WithFavicon("/favicon.ico")
                .WithTheme(ScalarTheme.BluePlanet)
                .AddPreferredSecuritySchemes("Bearer")
            );
        }

        return app;
    }
}