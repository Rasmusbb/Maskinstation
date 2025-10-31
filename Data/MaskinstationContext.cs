
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

            modelBuilder.Entity<Tag>()
            .HasIndex(t => t.TagName)
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

            modelBuilder.Entity<ImageTag>()
                .HasKey(it => new { it.ImageID, it.TagID });

            modelBuilder.Entity<ImageTag>()
                .HasOne(it => it.Image)
                .WithMany(i => i.Tags)
                .HasForeignKey(it => it.ImageID);


            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserID = Guid.Parse("2c08577b-c673-416e-031b-08ddfcc99d40"),
                Email = "Admin",
                Role = "Admin",
                Name = "Admin User",
                Password = "16360cfa006cf26f830fca8cd83f78472bebe5227cad28c01269fc807d061d7e",
            });
            modelBuilder.Entity<Tag>().HasData(
                new Tag
                {
                    TagID = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    TagName = "profilpicture",
                    TagType = TagType.User,
                    deletable = false
                }
            );
        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Machine> Machines { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default!;   
        public DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<Gallery> Galleries { get; set; } = default!;
        public DbSet<Image> Images { get; set; } = default!;

        public DbSet<Service> Services { get; set; } = default!;

    }
}
