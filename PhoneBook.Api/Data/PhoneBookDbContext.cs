using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.Data.Entity;

namespace PhoneBook.Api.Data
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Information> Informations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("person");
            modelBuilder.Entity<Person>().ToTable("persons");
            modelBuilder.Entity<Person>().HasMany(x => x.Emails);
            modelBuilder.Entity<Email>().ToTable("person_emails");

            modelBuilder.Entity<Person>().HasMany(x => x.Phones);
            modelBuilder.Entity<Phone>().ToTable("person_phones");

            modelBuilder.Entity<Person>().HasMany(x => x.Locations);
            modelBuilder.Entity<Location>().ToTable("person_locations");

            modelBuilder.Entity<Person>().HasMany(x => x.Informations);
            modelBuilder.Entity<Information>().ToTable("person_informations");
        }
    }
}
