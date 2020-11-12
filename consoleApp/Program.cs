using System;
using System.Linq;
using System.Reflection;
using datastore;
using model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFPosgres.consoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var builder = new ConfigurationBuilder();
            builder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            var configuration = builder.Build();

            var services = new ServiceCollection();

            services.AddOptions();

            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var constr = configuration.GetConnectionString("BdContext");
            services.AddDbContext<BloggingContext>(options =>
                options.UseNpgsql(constr, b => b.MigrationsAssembly(migrationsAssembly)));

            var serviceProvider = services.BuildServiceProvider();
            var bdConf = configuration.GetSection("Bildoktoren");

            var ctx = serviceProvider.GetService<BloggingContext>();
            var blogs = ctx.Blogs.ToList();
            foreach (var blog in blogs)
            {
                Console.WriteLine(blog.BlogId);
            }
        }
    }
}
