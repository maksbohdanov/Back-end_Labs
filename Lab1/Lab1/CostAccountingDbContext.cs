﻿using CostAccounting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CostAccounting
{
    public class CostAccountingDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Record> Records { get; set; }

        public CostAccountingDbContext(DbContextOptions<CostAccountingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .HasOne(r => r.User)
                .WithMany(u => u.Records)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Records)
                .HasForeignKey(r => r.CategoryId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .IsRequired(false);

            Seed(modelBuilder);
        }
        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Name = "Tom" },
                new User() { Id = 2, Name = "Nick" },
                new User() { Id = 3, Name = "Bob" },
                new User() { Id = 4, Name = "Lee" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, CategoryName = "Fruits", IsCustom = false },
                new Category() { Id = 2, CategoryName = "Vegetables", IsCustom = false },
                new Category() { Id = 3, CategoryName = "Water", IsCustom = false },
                new Category() { Id = 4, CategoryName = "Juice", IsCustom = false }
            );

            modelBuilder.Entity<Record>().HasData(
                new Record() { Id = 1, UserId = 1, CategoryId = 1, CreationTime = new DateTime(2022, 10, 6), Sum = 10 },
                new Record() { Id = 2, UserId = 1, CategoryId = 1, CreationTime = new DateTime(2022, 10, 6), Sum = 15 },
                new Record() { Id = 3, UserId = 1, CategoryId = 2, CreationTime = new DateTime(2022, 10, 6), Sum = 8 },
                new Record() { Id = 4, UserId = 1, CategoryId = 3, CreationTime = new DateTime(2022, 10, 6), Sum = 3 },
                new Record() { Id = 5, UserId = 2, CategoryId = 4, CreationTime = new DateTime(2022, 10, 5), Sum = 20 },
                new Record() { Id = 6, UserId = 2, CategoryId = 3, CreationTime = new DateTime(2022, 10, 5), Sum = 15 },
                new Record() { Id = 7, UserId = 2, CategoryId = 2, CreationTime = new DateTime(2022, 10, 5), Sum = 8 },
                new Record() { Id = 8, UserId = 3, CategoryId = 1, CreationTime = new DateTime(2022, 10, 7), Sum = 3 },
                new Record() { Id = 9, UserId = 3, CategoryId = 4, CreationTime = new DateTime(2022, 10, 7), Sum = 8 },
                new Record() { Id = 10, UserId = 4, CategoryId = 3, CreationTime = new DateTime(2022, 10, 8), Sum = 3 }
           );
        }  
    }
}
