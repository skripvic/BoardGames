﻿using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Presentation.Configurations;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        public DbSet<Collection> Collections => Set<Collection>();

        public DbSet<Game> Games => Set<Game>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CollectionConfiguration());
            modelBuilder.ApplyConfiguration(new GameConfiguration());
        }

    }
}
