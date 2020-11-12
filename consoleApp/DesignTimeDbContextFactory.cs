using datastore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace EFPosgres.consoleApp
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BloggingContext> 
    { 
        public BloggingContext CreateDbContext(string[] args) 
        {
            var current = Directory.GetCurrentDirectory();
            Console.WriteLine(current);
            //.AddJsonFile(@Directory.GetCurrentDirectory() + "/../MyCookingMaster.API/appsettings.json").Build();
            var cb = new ConfigurationBuilder().SetBasePath(current);
            cb
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var configuration = cb.Build();
            var builder = new DbContextOptionsBuilder<BloggingContext>(); 
            var connectionString = configuration.GetConnectionString("BdContext"); 
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            builder.UseNpgsql(connectionString, b => b.MigrationsAssembly(migrationsAssembly)); 
            return new BloggingContext(builder.Options); 
        } 
    }
}