using Microsoft.EntityFrameworkCore;
using WAFcc.Models;

namespace WAFcc.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Relation> Relation { get; set; }
        public DbSet<PersonRelation> PersonRelation { get; set; }
        public DbSet<FileContent> FileContent { get; set; }
        public DbSet<PersonPhoto> PersonPhoto { get; set; }
        public DbSet<PersonDocument> PersonDocument { get; set; }
        public DbSet<PersonName> PersonName { get; set; }
        //public DbSet<PersonActivity> PersonActivity { get; set; }
        //public DbSet<PersonBiography> PersonBiography { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            AddPersonDependencies(modelBuilder);
        }

        private void AddPersonDependencies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(e => e.Relations)
                .WithMany(e => e.Members)
                .UsingEntity<PersonRelation>(
                    q => q
                        .HasOne(r => r.Relation)
                        .WithMany(r => r.PersonRelations)
                        .HasForeignKey(r => new { r.RelationId }),
                    q => q
                        .HasOne(r => r.Person)
                        .WithMany(r => r.PersonRelations)
                        .HasForeignKey(r => new { r.PersonId }),
                    q =>
                    {
                        q.HasKey(t => new { t.RelationId, t.PersonId });
                    }
                );

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Photos)
                .WithOne()
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Files)
                .WithOne()
                .HasForeignKey(e => e.PersonId);

            modelBuilder.Entity<Person>()
                .HasOne(e => e.MainPhoto)
                .WithOne()
                .HasForeignKey<Person>(e => e.MainPhotoId);
        }
    }
}