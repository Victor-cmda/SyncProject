using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SyncMobile.Models;

namespace SyncMobile.Context
{
    public class SyncContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Visita> Visitas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Filename=SyncDbLite.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");

            bool dbExists = File.Exists(dbPath);
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.NomeProduto).IsRequired().HasMaxLength(255);
                entity.Property(p => p.ValorProduto).HasColumnType("decimal(18,2)");
                entity.Property(p => p.InformacaoTecnica).HasMaxLength(1000);
                entity.Property(p => p.Foto).IsRequired();
            });

            modelBuilder.Entity<Visita>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Data).IsRequired();
                entity.Property(v => v.Localizacao).IsRequired().HasMaxLength(500);
            });
        }

        public void EnsureDatabaseCreated()
        {
            try
            {
                this.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    } 
}

