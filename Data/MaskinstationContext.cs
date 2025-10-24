
using Microsoft.EntityFrameworkCore;
using Maskinstation.Models;

namespace Maskinstation.Data
{
    public class MaskinstationContext : DbContext
    {
        public MaskinstationContext (DbContextOptions<MaskinstationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
            modelBuilder.Entity<UserTags>()
            .HasKey(ut => new { ut.UserID, ut.TagID });

            modelBuilder.Entity<UserTags>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTags)
                .HasForeignKey(ut => ut.UserID);

            modelBuilder.Entity<MachineTags>()
                .HasKey(mt => new { mt.MachineID, mt.TagID });

            modelBuilder.Entity<MachineTags>()
                .HasOne(mt => mt.Machine)
                .WithMany(m => m.MachineTags)
                .HasForeignKey(mt => mt.MachineID);
            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserID = Guid.Parse("2c08577b-c673-416e-031b-08ddfcc99d40"),
                Email = "Admin",
                Role = "Admin",
                Name = "Admin User",
                Password = "16360cfa006cf26f830fca8cd83f78472bebe5227cad28c01269fc807d061d7e"
            });
        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Machine> Machines { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default!;   
        public DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<Image> Images { get; set; } = default!;

    }
}
