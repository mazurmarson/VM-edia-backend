using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class DataContext : DbContext 
     { 
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Photo> Photos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(c => c.Followers)
        .WithOne(e => e.Follower);

        modelBuilder.Entity<User>()
        .HasMany(c => c.FollowedUsers)
        .WithOne(e => e.FollowedUser);
    }
    }
}