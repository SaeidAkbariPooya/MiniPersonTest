using Microsoft.EntityFrameworkCore;
using MiniPerson.Domain.Entities;

namespace MiniPerson.Infrastructure.DataBase.Context
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
        {

        }
        public DbSet<Person> Persons => Set<Person>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50); 

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NationCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BirthDate)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}
