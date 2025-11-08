
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

            modelBuilder.Entity<Roles>().HasData( new Roles
            {
                RoleID = Guid.Parse("1c08577b-c673-416e-031b-08ddfcc99d40"),
                RoleName = "Admin"
            });

            modelBuilder.Entity<User>().HasData(
            new User
            {
                UserID = Guid.Parse("2c08577b-c673-416e-031b-08ddfcc99d40"),
                Email = "Admin",
                Name = "Admin User",
                Password = "16360cfa006cf26f830fca8cd83f78472bebe5227cad28c01269fc807d061d7e",
                hasLoggedin = false
            });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.HasData(
                    new { UsersUserID = Guid.Parse("2c08577b-c673-416e-031b-08ddfcc99d40"), RolesRoleID = Guid.Parse("1c08577b-c673-416e-031b-08ddfcc99d40") }
                ));

            modelBuilder.Entity<Gallery>().HasData(
                new Gallery
                {
                    GalleryID = Guid.Parse("cd74f600-05b6-4776-ba5e-1bfe3334e6d3"),
                    Name = "Brands",
                    deletable = false
                });

            modelBuilder.Entity<Gallery>().HasData(
                new Gallery
                {
                    GalleryID = Guid.Parse("4c67681d-d914-467c-8f9e-52e9181baeb6"),
                    Name = "Assets",
                    deletable = false
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
