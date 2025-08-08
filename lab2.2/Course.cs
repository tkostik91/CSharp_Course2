namespace DBWork {
    public class Course {
        public int Id {get; set;}
        public string Title {get; set;}
        public int Duration {get; set;}
        public string? Description {get; set;}

        public virtual List<Teacher> Teachers{get; set;}

        public virtual List<Student> Students{get; set;}
    }
}