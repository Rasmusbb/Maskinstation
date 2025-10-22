
using Microsoft.EntityFrameworkCore;
using Maskinstation.models;

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


        }


        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Machine> Machines { get; set; } = default!;
        public DbSet<Brand> Brands { get; set; } = default!;   
        public DbSet<Tag> Tags { get; set; } = default!;
        public DbSet<Image> images { get; set; } = default!;

    }
}
