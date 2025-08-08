using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBWork {
    public class ApplicationContext: DbContext {
       public DbSet<Course> Course {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = Program.config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}