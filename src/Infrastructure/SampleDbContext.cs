using CleanArchitectureSample.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureSample.Infrastructure
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected SampleDbContext(DbContextOptions contextOptions)
        : base(contextOptions)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("SEC_Users");
                entity.HasKey(nameof(User.UserId));
            });
            modelBuilder.Entity<CodeItem>(entity =>
            {
                entity.ToTable("SEC_Codes");
                entity.HasKey(nameof(CodeItem.CodeId));
                entity.Property(x => x.CodeId).IsRequired().HasMaxLength(50);
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CodeItem> CodeItems { get; set; }
    }
}
