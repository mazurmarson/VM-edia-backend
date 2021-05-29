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
        public DbSet<Reaction> Reactions {get; set;}
        public DbSet<Post> Posts {get; set; }
        public DbSet<Comment> Comments {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany(c => c.Followers)
        .WithOne(e => e.Follower);

        modelBuilder.Entity<User>()
        .HasMany(c => c.FollowedUsers)
        .WithOne(e => e.FollowedUser);

        //         modelBuilder.Entity<User>()
        // .Property(x => x.Login).IsRequired().HasMaxLength(40);
        
        //         modelBuilder.Entity<User>()
        // .Property(x => x.Mail).IsRequired().HasMaxLength(60);

        //         modelBuilder.Entity<User>()
        // .Property(x => x.Description).HasMaxLength(250);

        //      modelBuilder.Entity<Post>()
        // .Property(x => x.Description).HasMaxLength(200);

        // //       modelBuilder.Entity<Post>()
        // // .Property(x => x.Photos).IsRequired();

        //      modelBuilder.Entity<Comment>()
        // .Property(x => x.PostId).IsRequired();

        //         modelBuilder.Entity<Comment>()
        // .Property(x => x.UserId).IsRequired();

        //      modelBuilder.Entity<Comment>()
        // .Property(x => x.Content).IsRequired();

        //      modelBuilder.Entity<Reaction>()
        // .Property(x => x.PostId).IsRequired();

        //         modelBuilder.Entity<Reaction>()
        // .Property(x => x.UserId).IsRequired();

        //         modelBuilder.Entity<Reaction>()
        // .Property(x => x.IsPositive).IsRequired();

        //         modelBuilder.Entity<Follow>()
        // .Property(x => x.FollowedUserId).IsRequired();

        //         modelBuilder.Entity<Follow>()
        // .Property(x => x.FollowerId).IsRequired();


    }
    }
}