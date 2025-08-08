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

                var courses = db.Courses.ToList();
                // db.Teachers.Find(1).Courses.AddRange(courses);
                // db.Teachers.Find(2).Courses.AddRange(courses.GetRange(0,2));

                // for(int i=1; i<=4; i++){
                //     db.Students.Find(i).Courses.AddRange(courses.GetRange(0,i));
                // }
                
                // db.SaveChanges();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                var r = from t in db.Teachers orderby t.Name
                select new { Teacher = t, Courses = t.Courses};

                foreach (var t in r.ToList()){
                    Console.WriteLine(t.Teacher.Name);
                    foreach(var c in t.Courses){
                        Console.WriteLine($"\t{c.Title}");
                        var r2 = from s in db.Students
                        where s.Courses.Contains(c)
                        orderby s.Name
                        select s;
                        foreach(var s in r2){
                            Console.WriteLine($"\t\t{s.Name}");
                        }
                    }
                }
                //жадная загрузка
                // var teachers = db.Teachers
                //     .Include(t => t.Courses)
                //     .ThenInclude(c => c.Students)
                //     .OrderBy(t => t.Name);
                // foreach(var t in teachers){
                //     Console.WriteLine($"{t.Name}");
                //     foreach(var c in t.Courses){
                //         Console.WriteLine($"\t{c.Title}");
                //         foreach(var s in c.Students){
                //             Console.WriteLine($"\t\t{s.Name}");
                //         }
                //     }
                    
                // }
                //явная загрузка
                // var teachers = db.Teachers
                //     .OrderBy(t => t.Name).ToList();
                // foreach(var t in teachers){
                //     Console.WriteLine($"{t.Name}");
                //     db.Entry(t).Collection(t1 => t1.Courses).Load();
                //     foreach(var c in t.Courses){
                //         Console.WriteLine($"\t{c.Title}");
                //         db.Entry(c).Collection(c1 => c1.Students).Load();
                //         foreach(var s in c.Students){
                //             Console.WriteLine($"\t\t{s.Name}");
                //         }
                //     }
                    
                // }
                //ленивая загрузка
                var teachers = db.Teachers
                    .OrderBy(t => t.Name).ToList();
                foreach(var t in teachers){
                    Console.WriteLine($"{t.Name}");
                    foreach(var c in t.Courses){
                        Console.WriteLine($"\t{c.Title}");
                        foreach(var s in c.Students){
                            Console.WriteLine($"\t\t{s.Name}");
                        }
                    }
                    
                }

            }

            // using (ApplicationContext db = new ApplicationContext())
            // {
            //     Course cx = db.Courses.OrderBy(c=>c.Id).LastOrDefault();
            //     db.Courses.Remove(cx);
            //     int changedRecords = db.SaveChanges(); // db.SaveChangesAsync();
            //     Console.WriteLine($"changed records: {changedRecords}");
            // }

            // using (ApplicationContext db = new ApplicationContext())
            // {
            //     var courses = from c in db.Courses
            //                  where c.Id == 3
            //                  select c;

            //     Console.WriteLine("People list with age < 40:");
            //     foreach (Course course in courses) {
            //         course.Title="Курс 3";
            //         course.Duration=666;
            //     }
            //     int changedRecords = db.SaveChanges();
                   

            // }
            //  using (ApplicationContext db = new ApplicationContext())
            // {
            //     var courses = db.Courses.ToList();
            //     Console.WriteLine("People list:");
            //     foreach(Course c in courses){
            //         Console.WriteLine($"Person id={c.Id}, Title={c.Title}, Duration={c.Duration}, Description={c.Description}");
            //     }
            // }
        }
   }
  
}