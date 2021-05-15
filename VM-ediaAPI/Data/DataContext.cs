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
    }
}