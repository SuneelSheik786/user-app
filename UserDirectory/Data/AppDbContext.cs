using Microsoft.EntityFrameworkCore;
using UserDirectory.Models;

namespace UserDirectory.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Alice", Age = 30, City = "Chennai", State = "TN", Pincode = "600001" },
                new User { Id = 2, Name = "Bob", Age = 25, City = "Bangalore", State = "KA", Pincode = "560001" }
            );
        }
    }
}