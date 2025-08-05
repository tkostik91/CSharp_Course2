namespace Lab1
{

    class Sync
    {
        public double x = 1;
        public bool phase = true;

    }

    class MyThread
    {
        public Sync s;
        int start;
        int end;

        public MyThread(int start = 0, int end = 0)
        {
            this.start = start;
            this.end = end;
        }

        public MyThread(Sync s)
        {
            this.s = s;
        }

        public void printRange()
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Thread={Thread.CurrentThread.Name} [i]={i}");
            }
        }

        public void printRangeWithJoin(object? o)
        {
            if (o is Thread t)
            {
                while (t.ThreadState == ThreadState.Unstarted) Thread.Yield();
                t.Join(10_000);
            }
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Thread={Thread.CurrentThread.Name} [i]={i}");
            }
        }

        public void Cosinus()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (s)
                {
                    while (!s.phase)
                        Monitor.Wait(s);
                
                    s.x = Math.Cos(s.x);
                    s.phase = !s.phase;
                    Console.WriteLine($"Cos={s.x}");
                    Monitor.Pulse(s);
                }

            }
        }

        public void ArgCosinus()
        {


            for (int i = 0; i < 10; i++)
            {
                lock (s)
                {
                    while (s.phase)
                        Monitor.Wait(s);
                
                    s.x = Math.Acos(s.x);
                    s.phase = !s.phase;
                    Console.WriteLine($"ArgCos={s.x}");
                    Monitor.Pulse(s);
                }
            }
        }




    }
    internal class Program
    {


        static void Main()
        {
            Sync s = new Sync();

            Thread t1 = new Thread(new MyThread(s).Cosinus) { Name = "Thread1" };
            Thread t2 = new Thread(new MyThread(s).ArgCosinus) { Name = "Thread2" };

            t1.Start();
            t2.Start();
        }

    }
}