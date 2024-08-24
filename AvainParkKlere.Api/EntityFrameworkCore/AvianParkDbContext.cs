using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace AvainParkKlere.Api.EntityFrameworkCore
{
    public class AvianParkDbContext : DbContext
    {
        public AvianParkDbContext(DbContextOptions<AvianParkDbContext> options)
    : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Clothing> Clothes { get; set; }
        public DbSet<StudentClothing> StudentClothes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(s => s.HasKey(x => x.Id));

            builder.Entity<Clothing>(c => c.HasKey(x => x.Id));

            builder.Entity<StudentClothing>(sc =>
            {
                sc.HasKey(sc => sc.Id);

                sc.HasOne<Student>().WithMany()
                    .HasForeignKey(x => x.StudentId).IsRequired();

                sc.HasOne<Clothing>().WithMany()
                    .HasForeignKey(x => x.ClothingId).IsRequired();
            }); 
        }

    }
}
