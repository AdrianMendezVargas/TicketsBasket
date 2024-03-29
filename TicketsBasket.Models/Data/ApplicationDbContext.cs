﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketsBasket.Models.Domain;

namespace TicketsBasket.Models.Data {
    public class ApplicationDbContext : DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventImage> EventImages { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<JobApplcation> JobApplcations { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WishListEvent> WishListEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.Events)
                .WithOne(p => p.UserProfile)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.WishListEvents)
                .WithOne(p => p.UserProfile)
                .OnDelete(DeleteBehavior.NoAction);   
            
            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.Likes)
                .WithOne(p => p.UserProfile)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.Tickets)
                .WithOne(p => p.UserProfile)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.SenApplications)
                .WithOne(p => p.AppliedUser)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserProfile>()
                .HasMany(p => p.RecievedApplications)
                .WithOne(p => p.Organizer)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasMany(p => p.EventTags)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasMany(p => p.EventImages)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasMany(p => p.Likes)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasMany(p => p.Tickets)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Event>()
                .HasMany(p => p.WishListEvents)
                .WithOne(p => p.Event)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

    }
}
