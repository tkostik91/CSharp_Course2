namespace Lab1{
    class MyThread{
        int start;
        int end;

        public MyThread(int start=0, int end=0){
            this.start =start;
            this.end = end;
        }

        public void printRange() {
            for(int i = start; i <= end; i++){
                Console.WriteLine($"Thread={Thread.CurrentThread.Name} [i]={i}");
            }
        }

        public void printRangeWithJoin(object? o) {
            if(o is Thread t) {
                while (t.ThreadState == ThreadState.Unstarted) Thread.Yield();
                t.Join(10_000);
            }
            for(int i = start; i <= end; i++){
                Console.WriteLine($"Thread={Thread.CurrentThread.Name} [i]={i}");
            }
        }


    }   
    internal class Program {
        
        static void Main(){
           Thread t1 = new Thread(new MyThread(1,100).printRange){Name="Thread1"};
           Thread t2 = new Thread(new MyThread(1,100).printRangeWithJoin){Name="Thread2"};

           t2.Start(t1);
           Thread.Sleep(1000);
           t1.Start();
           
        //    Console.WriteLine("------------------");
        //    t1.Start();
        //    t2.Start(t1);
        }
    
    }
}