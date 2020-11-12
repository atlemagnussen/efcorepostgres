using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace datastore
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BloggingContext> 
    { 
        public BloggingContext CreateDbContext(string[] args) 
        {
            var current = Directory.GetCurrentDirectory();
            var webFolder = Path.Combine(current, "..", "web");
            Console.WriteLine(webFolder);
            Console.WriteLine(current);
            var appSettingsPath = Path.Combine(webFolder, "appsettings.json");
            var appSettingsDevPath = Path.Combine(webFolder, "appsettings.Development.json");
            var cb = new ConfigurationBuilder().SetBasePath(current);
            cb
                .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
                .AddJsonFile(appSettingsDevPath, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var configuration = cb.Build();
            var builder = new DbContextOptionsBuilder<BloggingContext>(); 
            var connectionString = configuration.GetConnectionString("BdContext"); 
            builder.UseNpgsql(connectionString); 
            return new BloggingContext(builder.Options); 
        } 
    }
}