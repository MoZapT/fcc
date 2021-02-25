using Microsoft.EntityFrameworkCore;
using WebAppFcc.Shared.Models;

namespace WebAppFcc.Repository
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Relation> Relation { get; set; }
        public DbSet<FileContent> FileContent { get; set; }
        //public DbSet<PersonName> PersonName { get; set; }
        //public DbSet<PersonDocument> PersonDocument { get; set; }
        //public DbSet<PersonActivity> PersonActivity { get; set; }
        //public DbSet<PersonBiography> PersonBiography { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddPersonDependencies(modelBuilder);

            //modelBuilder.Entity<Relation>()
            //    .HasOne(e => e.Inviter)
            //    .WithOne()
            //    .HasForeignKey<Relation>(e => e.InviterId);

            //modelBuilder.Entity<Relation>()
            //    .HasOne(e => e.Invited)
            //    .WithOne()
            //    .HasForeignKey<Relation>(e => e.InvitedId);
        }

        private void AddPersonDependencies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(e => e.InviterRelations)
                .WithOne(e => e.Inviter)
                .HasForeignKey(e => e.InviterId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.InvitedRelations)
                .WithOne(e => e.Invited)
                .HasForeignKey(e => e.InvitedId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Files)
                .WithOne()
                .HasForeignKey(e => e.PersonId);

            //modelBuilder.Entity<Person>()
            //    .HasMany(e => e.Photos)
            //    .WithOne()
            //    .HasForeignKey(e => e.PersonId);

            //modelBuilder.Entity<Person>()
            //    .HasOne(e => e.MainPhoto)
            //    .WithOne()
            //    .HasForeignKey<PersonFileContent>(e => e.PersonId);
        }
    }
}