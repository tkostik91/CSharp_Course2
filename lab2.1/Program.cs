using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DBWork {
   internal class Program {
       internal static IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json").Build();

        static void Main(string[] Args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.EnsureCreated();

                Course c1 = new Course {Title="Курс 1", Duration=5, Description = "Описание 1"};
                Course c2 = new Course {Title="Курс 2", Duration=5, Description = "Описание 2"};

                db.Course.AddRange(c1,c2);
                db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                Course cx = db.Course.OrderBy(c=>c.Id).LastOrDefault();
                db.Course.Remove(cx);
                int changedRecords = db.SaveChanges(); // db.SaveChangesAsync();
                Console.WriteLine($"changed records: {changedRecords}");
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var courses = from c in db.Course
                             where c.Id == 3
                             select c;

                Console.WriteLine("People list with age < 40:");
                foreach (Course course in courses) {
                    course.Title="Курс 3";
                    course.Duration=666;
                }
                int changedRecords = db.SaveChanges();
                   

            }
             using (ApplicationContext db = new ApplicationContext())
            {
                var courses = db.Course.ToList();
                Console.WriteLine("People list:");
                foreach(Course c in courses){
                    Console.WriteLine($"Person id={c.Id}, Title={c.Title}, Duration={c.Duration}, Description={c.Description}");
                }
            }
        }
   }
  
}