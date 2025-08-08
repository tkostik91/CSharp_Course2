using System.Diagnostics;

const int STEPS = 100_000_000;
const int TASKS = 10;
double Single(Func<double,double> f, double a, double b, int steps = STEPS)
{
    double w = (b - a) / steps;
    double summa = 0d;
    for (int i = 0; i < steps; i++)
    { 
        double x = a + i * w +w / 2;
        double h = f(x);
        summa += h * w;
    }
    return summa;
}

double ParallelProccess(Func<double,double> f, double a, double b)
{
    double w = (b - a) / TASKS;
    double summa = 0d;
    var s = new object();

    Parallel.For(0, TASKS,
    i => {
        double r = Single(f, a + i * w, a + (i+1) * w, STEPS / TASKS);
        lock(s) summa += r;
    });

    return summa;
}

Stopwatch t1 = new Stopwatch();
t1.Start();
double r1 = Single(Math.Sin, 0, Math.PI / 2);
t1.Stop();

Console.WriteLine($"Single result : {r1} Time: {t1.ElapsedMilliseconds}");

Stopwatch t2 = new Stopwatch();
t2.Start();
r1 = ParallelProccess(Math.Sin, 0, Math.PI / 2);
t2.Stop();

Console.WriteLine($"Parallel result : {r1} Time: {t2.ElapsedMilliseconds}");