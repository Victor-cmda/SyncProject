using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SyncAPI.Models;

namespace SyncAPI.Data
{
    public class SyncAPIDbContext : DbContext
    {
        public SyncAPIDbContext(DbContextOptions<SyncAPIDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Visitas> Visitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Visitas>().HasKey(v => v.Id);
        }
    }
}
