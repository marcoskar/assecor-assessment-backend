using Microsoft.EntityFrameworkCore;
using PersonColor.Api.Models;

namespace PersonColor.Api.Repositories
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public PersonDbContext() : base(new DbContextOptionsBuilder<PersonDbContext>()
            .UseSqlite("Data Source=persons.db").Options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.ZipCode).IsRequired();
                entity.Property(e => e.City).IsRequired();
                entity.Property(e => e.Color).IsRequired();
            });
        }
    }
}
