using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace DBWork {
    public class ApplicationContext: DbContext {
       public DbSet<Course> Courses {get; set;} = null!;
       public DbSet<Student> Students {get; set;} = null!;
       public DbSet<Teacher> Teachers {get; set;} = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            string connectionString = Program.config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.LogTo(s=>Debug.WriteLine(s));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().HasData(
                new Course {Id = 1, Title="Курс 1", Duration=1, Description="Описание 1"},
                new Course {Id = 2, Title="Курс 2", Duration=2, Description="Описание 2"},
                new Course {Id = 3, Title="Курс 3", Duration=3, Description="Описание 3"},
                new Course {Id = 4, Title="Курс 4", Duration=4, Description="Описание 4"}
            );
            modelBuilder.Entity<Student>().HasData(
                new Student {Id = 1, Name="Имя 1", Age=21},
                new Student {Id = 2, Name="Имя 2", Age=22},
                new Student {Id = 3, Name="Имя 3", Age=23},
                new Student {Id = 4, Name="Имя 4", Age=24}
            );
             modelBuilder.Entity<Teacher>().HasData(
                new Teacher {Id = 1, Name="Имя препода 1"},
                new Teacher {Id = 2, Name="Имя препода 2"},
                new Teacher {Id = 3, Name="Имя препода 3"},
                new Teacher {Id = 4, Name="Имя препода 4"}
            );
        }
    }
}