﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Repository
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<PersonActivity> PersonActivity { get; set; }
        public DbSet<PersonBiography> PersonBiography { get; set; }
        public DbSet<PersonRelation> PersonRelation { get; set; }
        public DbSet<Relation> Relation { get; set; }
        public DbSet<PersonName> PersonName { get; set; }
        public DbSet<PersonDocument> PersonDocument { get; set; }
        public DbSet<FileContent> FileContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PersonRelation>()
                .HasKey(e => new { e.RelationId, e.PersonId });
        }
    }
}