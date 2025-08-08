namespace DBWork {
    public class Student {
        public int Id {get; set;}
        public string Name {get; set;}
        public int Age {get; set;}
        public virtual List<Course> Courses{get; set;}
    }
}