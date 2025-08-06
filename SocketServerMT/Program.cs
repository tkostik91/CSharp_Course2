using System.Net;
using System.Net.Sockets;
using System.Text;

const int MAX_CONNECTION_IN_QUEUE = 10; // 10
const int MAX_THREADS = 20;

//ThreadPool.SetMinThreads(MAX_CONNECTION_IN_QUEUE, MAX_CONNECTION_IN_QUEUE);
ThreadPool.SetMaxThreads(MAX_THREADS, MAX_THREADS);

IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 1111);
using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(ipPoint);
socket.Listen(MAX_CONNECTION_IN_QUEUE);
int req = 0;
var s = new object();
while (true)
{
    Socket client = socket.Accept();

   Task.Run(() =>
    {
    try
    {
        Console.Write($"Remote client: {client.RemoteEndPoint} ");
            using var stream = new NetworkStream(client);
            using var r = new StreamReader(stream, Encoding.UTF8);
            using var w = new StreamWriter(stream, Encoding.UTF8);
            string result = r.ReadLine();
            lock (s) req++;
            Console.WriteLine($"Received: {result}, Requests: {req}");
            Thread.Sleep(100); //

            w.WriteLine(result.ToUpper());
            w.Flush();
            client.Dispose();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    });
}

