using ApiAuth.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAuth.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(p =>
            {
                p.HasKey(p => p.Id);

                p.Property(p => p.Id).IsRequired();

                p.Property(p => p.Name).IsRequired()
                 .HasColumnType("varchar(45)");

                p.Property(p => p.Username).IsRequired()
                .HasColumnType("varchar(45)");

                p.Property(p => p.Password).IsRequired()
                .HasColumnType("varchar(45)");

            });
        }
    }
}
