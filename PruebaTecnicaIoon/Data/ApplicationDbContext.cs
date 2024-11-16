﻿using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIoon.modelos;
using System;

namespace PruebaTecnicaIoon.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Commerce> Commerces { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para State
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(s => s.StateId);
                entity.Property(s => s.StateId).ValueGeneratedOnAdd();
                entity.Property(s => s.StateName).IsRequired().HasMaxLength(50);
            });

            // Configuración para Commerce
            modelBuilder.Entity<Commerce>(entity =>
            {
                entity.HasKey(c => c.CommerceId);
                entity.Property(c => c.CommerceId).ValueGeneratedOnAdd();
                entity.Property(c => c.CommerceName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Address).HasMaxLength(200);
                entity.Property(c => c.RUC).HasMaxLength(13);
                entity.HasOne(c => c.State).WithMany().HasForeignKey(c => c.StateId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId).ValueGeneratedOnAdd();
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Role).IsRequired().HasMaxLength(20);
                entity.HasOne(u => u.State).WithMany().HasForeignKey(u => u.StateId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(u => u.Commerce).WithMany(c => c.Users).HasForeignKey(u => u.CommerceId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para Sale
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.SaleId);
                entity.Property(s => s.SaleId).ValueGeneratedOnAdd();
                entity.Property(s => s.SaleDate).IsRequired();
                entity.HasOne(s => s.User).WithMany(u => u.Sales).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.Commerce).WithMany(c => c.Sales).HasForeignKey(s => s.CommerceId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(s => s.State).WithMany().HasForeignKey(s => s.StateId).OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para SaleDetail
            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.HasKey(sd => sd.DetailId);
                entity.Property(sd => sd.DetailId).ValueGeneratedOnAdd();
                entity.Property(sd => sd.Product).IsRequired().HasMaxLength(100);
                entity.Property(sd => sd.Quantity).IsRequired();
                entity.Property(sd => sd.Price).IsRequired().HasColumnType("DECIMAL(18,2)");
                entity.HasOne(sd => sd.Sale).WithMany(s => s.SaleDetails).HasForeignKey(sd => sd.SaleId).OnDelete(DeleteBehavior.Cascade);
            });

            
        }
    }
}