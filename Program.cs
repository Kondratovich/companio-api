using Companio;

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseConfiguration(new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build());

            webBuilder.UseStartup<Startup>();
        });
}