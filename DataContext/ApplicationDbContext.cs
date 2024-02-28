using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
            {
            
            }
        public DbSet<Employee>Employees { get; set; }

        // to seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
