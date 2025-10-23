
using Microsoft.EntityFrameworkCore;
using Maskinstation.models;
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

        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Machine> Machines { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default!;   
        public DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<Image> Images { get; set; } = default!;

    }
}
