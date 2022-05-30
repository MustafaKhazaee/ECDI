using Microsoft.EntityFrameworkCore;

namespace ECDI.Models {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base (options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
