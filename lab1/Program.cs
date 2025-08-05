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

    }   
    internal class Program {
        
        static void Main(){
           Thread t1 = new Thread(new MyThread(1,10).printRange){Name="Thread1"};
           Thread t2 = new Thread(new MyThread(11,20).printRange){Name="Thread2"};

           t1.Start();
           t2.Start();
        }
    
    }
}