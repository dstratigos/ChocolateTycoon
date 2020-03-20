﻿using ChocolateTycoon.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChocolateTycoon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<MainStorage> MainStorages { get; set; }
        public DbSet<Safe> Safes { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Chocolate> Chocolates { get; set; }
        public DbSet<ProductionUnit> ProductionUnits { get; set; }
        public DbSet<StorageUnit> StorageUnits { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public ApplicationDbContext()
            : base("ChocolateTycoonContext", throwIfV1Schema: false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Factories);

            modelBuilder.Entity<Factory>()
                .HasOptional(f => f.Supplier);
                
            
            base.OnModelCreating(modelBuilder);
        }
    }
}