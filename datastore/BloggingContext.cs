using Microsoft.EntityFrameworkCore;
using model;
using System;
using System.Collections.Generic;

namespace datastore
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(
            DbContextOptions<BloggingContext> options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseNpgsql("Data Source=blogging.db");
    }
}
